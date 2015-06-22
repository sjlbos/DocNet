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
    public class CsTextParser : ICsParser
    {
        private readonly SourceText _csText;

        public CsTextParser(Stream fileStream)
        {
            _csText = SourceText.From(fileStream);
        }

        public CsTextParser(string fileText)
        {
            _csText = SourceText.From(fileText);
        }

        public IEnumerable<NamespaceModel> GetNamespaceTrees()
        {
            if(_csText == null)
                return null;

            SyntaxTree tree = CSharpSyntaxTree.ParseText(_csText);
            var walker = new CsCommentWalker();
            walker.Visit(tree.GetRoot());
            return walker.NamespaceModels;
        }
    }

    internal class CsCommentWalker : CSharpSyntaxWalker
    {
        public IList<NamespaceModel> NamespaceModels { get; private set; }

        private NamespaceModel _currentNamespace;
        private InterfaceModel _currentInterface;

        public CsCommentWalker()
        {
            NamespaceModels = new List<NamespaceModel>();    
        }

        #region Node Processors

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            _currentNamespace = new NamespaceModel {Name = node.Name.ToString()};
            NamespaceModels.Add(_currentNamespace);
            base.VisitNamespaceDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var currentInterface = new InterfaceModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };
            _currentInterface = currentInterface;
            _currentNamespace.Interfaces.Add(currentInterface);
            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var currentClass = new ClassModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };
            _currentInterface = currentClass;
            _currentNamespace.Classes.Add(currentClass);
            base.VisitClassDeclaration(node);
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var currentEnum = new EnumModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                Parent = _currentInterface,
                DocComment = GetCommentFromNode<DocComment>(node)
            };
            if(_currentInterface != null)
                AddTypeToParent(currentEnum);
            else
                _currentNamespace.Enums.Add(currentEnum);
            _currentNamespace.Enums.Add(currentEnum);
            base.VisitEnumDeclaration(node);
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            var currentStruct = new StructModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                Parent = _currentInterface,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };
            if(_currentInterface != null)
                AddTypeToParent(currentStruct);
            else
                _currentNamespace.Structs.Add(currentStruct);
            base.VisitStructDeclaration(node);
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
            if(_currentInterface != null)
                AddTypeToParent(currentDelegate);
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

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            base.VisitPropertyDeclaration(node);
        }

        #endregion

        #region Helper Methods

        private void AddTypeToParent(CsTypeModel type)
        {
            if(_currentInterface is ClassModel)
            {
                ((ClassModel) _currentInterface).NestedTypes.Add(type);
            }
            else if(_currentInterface is StructModel)
            {
                ((StructModel) _currentInterface).NestedTypes.Add(type);
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
