using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocNet.Core.Exceptions;
using DocNet.Models.Comments;
using DocNet.Models.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace DocNet.Core.Parsers.CSharp
{
    /// <summary>
    /// This class is responsible for parsing C# source code into an internal DocNet representation of the namespaces, types,
    /// and doc comments contained in that source code.
    /// </summary>
    public class CsTextParser : ICsParser
    {
        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        public NamespaceModel GetGlobalNamespace(string sourceCode)
        {
            if(sourceCode == null)
                throw new ArgumentNullException("sourceCode");
            var sourceText = SourceText.From(sourceCode);
            return GetGlobalNamespace(sourceText);
        }

        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        public NamespaceModel GetGlobalNamespace(Stream sourceFileStream)
        {
            if(sourceFileStream == null)
                throw new ArgumentNullException("sourceFileStream");
            var sourceText = SourceText.From(sourceFileStream);
            return GetGlobalNamespace(sourceText);
        }

        private NamespaceModel GetGlobalNamespace(SourceText csText)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(csText);
            var walker = new CsCommentWalker();
            walker.Visit(tree.GetRoot());
            return walker.GlobalNamespace;
        }
    }

    internal class CsCommentWalker : CSharpSyntaxWalker
    {
        public NamespaceModel GlobalNamespace { get; private set; }

        private NamespaceModel _currentNamespace;
        private InterfaceModel _currentInterface;

        public CsCommentWalker()
        {
            GlobalNamespace = new NamespaceModel(); 
        }

        #region Node Processors

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var newNamespace = new NamespaceModel
            {
                Name = node.Name.ToString(),
                ParentNamespace = _currentNamespace
            };
            var parentNamespace = _currentNamespace;
            _currentNamespace = newNamespace;
            if(parentNamespace == null)
                GlobalNamespace.ChildNamespaces.Add(newNamespace);
            else
                parentNamespace.ChildNamespaces.Add(newNamespace);
            base.VisitNamespaceDeclaration(node);
            _currentNamespace = parentNamespace;
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var newInterface = new InterfaceModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };

            if (TypeIsNested())
                LinkTypeToParent(newInterface);
            else
                _currentNamespace.Interfaces.Add(newInterface); 
            
            var parentInterface = _currentInterface;
            _currentInterface = newInterface;         
            base.VisitInterfaceDeclaration(node);
            _currentInterface = parentInterface;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var newClass = new ClassModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };

            if(TypeIsNested())
                LinkTypeToParent(newClass);
            else
                _currentNamespace.Classes.Add(newClass);

            var parentInterface = _currentInterface;
            _currentInterface = newClass;     
            base.VisitClassDeclaration(node);
            _currentInterface = parentInterface;
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            var newStruct = new StructModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };

            if(TypeIsNested())
                LinkTypeToParent(newStruct);
            else
                _currentNamespace.Structs.Add(newStruct);

            var previousInterface = _currentInterface;
            _currentInterface = newStruct;
            base.VisitStructDeclaration(node);
            _currentInterface = previousInterface;
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var newEnum = new EnumModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<DocComment>(node)
            };
            if (TypeIsNested())
                LinkTypeToParent(newEnum);
            else
                _currentNamespace.Enums.Add(newEnum);
            base.VisitEnumDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            var currentDelegate = new DelegateModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                Parent = _currentInterface,
                DocComment = GetCommentFromNode<MethodDocComment>(node)
            };

            if(TypeIsNested())
                LinkTypeToParent(currentDelegate);
            else
                _currentNamespace.Delegates.Add(currentDelegate);

            base.VisitDelegateDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            base.VisitMethodDeclaration(node);
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            base.VisitParameter(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            base.VisitPropertyDeclaration(node);
        }

        #endregion

        #region Helper Methods

        private bool TypeIsNested()
        {
            return _currentInterface != null;
        }

        private void LinkTypeToParent(CsTypeModel type)
        {
            type.Parent = _currentInterface;
            if (_currentInterface == null) return;

            if(_currentInterface is ClassAndStructModel)
            {
                ((ClassAndStructModel) _currentInterface).NestedTypes.Add(type);
            }
            else
            {
                throw new CsParsingException("Type encountered inside of non-nestable type.");
            }
        }

        private T GetCommentFromNode<T>(SyntaxNode node) where T : DocComment
        {
            var docCommentTrivia =
                node.GetLeadingTrivia()
                    .Where(
                        t =>
                            t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                            t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia));

            var docComment = docCommentTrivia.FirstOrDefault();
            if(docComment == null) return null;

            string commentXmlString = StripTripleSlashesFromComment(docComment.ToFullString());
            return DocComment.FromXml<T>(commentXmlString);
        }

        private string StripTripleSlashesFromComment(string xmlComment)
        {
            Regex tripleSlashRegex = new Regex(@"(\r?\n)?\s?///");
            return tripleSlashRegex.Replace(xmlComment, String.Empty);
        }

        #endregion
    }
}
