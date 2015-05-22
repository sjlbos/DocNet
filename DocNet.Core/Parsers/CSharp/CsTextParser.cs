using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
                yield break;

            SyntaxTree tree = CSharpSyntaxTree.ParseText(_csText);
            var namespaceNodes = tree.GetRoot().DescendantNodes().OfType<NamespaceDeclarationSyntax>();
            foreach (var namespaceNode in namespaceNodes)
            {
               yield return GetNamespaceModelFromNode(namespaceNode);
            }
        }

        #region Model Factories

        private NamespaceModel GetNamespaceModelFromNode(NamespaceDeclarationSyntax namespaceNode)
        {
            NamespaceModel model = new NamespaceModel();
            foreach (var classDeclaration in namespaceNode.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {
                model.Classes.Add(GetClassModelFromNode(classDeclaration));
            }
            return model;
        }

        private ClassModel GetClassModelFromNode(ClassDeclarationSyntax classNode)
        {
            ClassModel model = new ClassModel
            {
                ClassName = classNode.Identifier.Text,
                DocComment = GetCommentFromNode<ClassDocComment>(classNode)
            };
            return model;
        }

        #endregion

        #region Comment Factories

        private T GetCommentFromNode<T>(SyntaxNode node) where T : DocComment
        {
            var docComment = node.GetLeadingTrivia().OfType<DocumentationCommentTriviaSyntax>().SingleOrDefault();
            if (docComment == null) return null;

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
