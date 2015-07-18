using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocNet.Core.Exceptions;
using DocNet.Core.Models.Comments;
using DocNet.Core.Models.CSharp;
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
        /// Parses C# source code and adds the parsed elements to the specified namespace.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <param name="parentNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        public void ParseIntoNamespace(string sourceCode, NamespaceModel parentNamespace)
        {
            if(sourceCode == null)
                throw new ArgumentNullException("sourceCode");
            if(parentNamespace == null)
                throw new ArgumentNullException("parentNamespace");
            var sourceText = SourceText.From(sourceCode);
            ParseIntoNamespace(sourceText, parentNamespace);
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

        /// <summary>
        /// Parses C# source code and adds the parsed elments to the specified namespace.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <param name="parentNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        public void ParseIntoNamespace(FileStream sourceFileStream, NamespaceModel parentNamespace)
        {
            if (sourceFileStream == null)
                throw new ArgumentNullException("sourceFileStream");
            if (parentNamespace == null)
                throw new ArgumentNullException("parentNamespace");
            var sourceText = SourceText.From(sourceFileStream);
            ParseIntoNamespace(sourceText, parentNamespace);
        }

        private static NamespaceModel GetGlobalNamespace(SourceText csText)
        {
            var namespaceModel = new NamespaceModel();
            ParseIntoNamespace(csText, namespaceModel);
            return namespaceModel;
        }

        private static void ParseIntoNamespace(SourceText csText, NamespaceModel namespaceModel)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(csText);
            var walker = new CsCommentWalker(namespaceModel);
            walker.Visit(tree.GetRoot());
        }
    }

    internal class CsCommentWalker : CSharpSyntaxWalker
    {
        private ICsParentElement _currentParent;

        public CsCommentWalker(NamespaceModel globalNamespace)
        {
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            _currentParent = globalNamespace;
        }

        #region Node Processors

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            NamespaceModel currentNamespace;
            string namespaceName = node.Name.ToString();

            // Create a new namespace model if the current namespace has not yet been encountered
            if(_currentParent[namespaceName] == null)
            {
                currentNamespace = new NamespaceModel { Name = namespaceName }; 
                _currentParent.AddChild(currentNamespace);
            }
            // Otherwise, use the existing namespace
            else
            {
                if(!(_currentParent[namespaceName] is NamespaceModel)) 
                    throw new NamingCollisionException(namespaceName);

                currentNamespace = _currentParent[namespaceName] as NamespaceModel;
            }     

            var oldParent = _currentParent;
            _currentParent = currentNamespace;

            base.VisitNamespaceDeclaration(node);

            _currentParent = oldParent;
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var currentInterface = new InterfaceModel
            {
                Name = node.Identifier.Text,
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList),
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };
            SetInterfaceBaseModifiers(currentInterface, node.Modifiers);

            if(currentInterface.IsPartial)
            {
                if(!MergeExistingBaseInterfaceDeclarations(ref currentInterface))
                {
                    // Interface was not merged and therefore has not yet been encountered
                    _currentParent.AddChild(currentInterface);
                }           
            }
            else
            {
                _currentParent.AddChild(currentInterface);
            }
           
            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentInterface;
       
            base.VisitInterfaceDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var currentClass = new ClassModel
            {
                Name = node.Identifier.Text,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList)
            };
            SetClassModifiers(currentClass, node.Modifiers);

            if (currentClass.IsPartial)
            {
                if (!MergeExistingBaseInterfaceDeclarations(ref currentClass))
                {
                    // Class was not merged and therefore has not yet been encountered
                    _currentParent.AddChild(currentClass);
                }
            }
            else
            {
                _currentParent.AddChild(currentClass);
            }

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentClass;
             
            base.VisitClassDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var currentStruct = new StructModel
            {
                Name = node.Identifier.Text,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList)
            };
            SetInterfaceBaseModifiers(currentStruct, node.Modifiers);

            if (currentStruct.IsPartial)
            {
                if (!MergeExistingBaseInterfaceDeclarations(ref currentStruct))
                {
                    // Class was not merged and therefore has not yet been encountered
                    _currentParent.AddChild(currentStruct);
                }
            }
            else
            {
                _currentParent.AddChild(currentStruct);
            }

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentStruct;

            base.VisitStructDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newEnum = new EnumModel
            {
                Name = node.Identifier.Text,
                AccessModifier = GetAccessModifier(node.Modifiers),
                DocComment = GetCommentFromNode<DocComment>(node)
            };

            if(_currentParent != null)
                _currentParent.AddChild(newEnum);

            base.VisitEnumDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newDelegate = new DelegateModel
            {
                Name = node.Identifier.Text,
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString(),
                DocComment = GetCommentFromNode<MethodDocComment>(node),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses)
            };

            if(_currentParent != null)
                _currentParent.AddChild(newDelegate);

            base.VisitDelegateDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newConstructor = new ConstructorModel
            {
                Name = node.Identifier.Text,
                DocComment = GetCommentFromNode<MethodDocComment>(node),
                Parameters = GetParameterList(node.ParameterList.Parameters)
            };

            SetConstructorModifiers(newConstructor, node.Modifiers); 

            _currentParent.AddChild(newConstructor);

            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var newMethod = new MethodModel
            {
                Name = node.Identifier.Text,
                DocComment = GetCommentFromNode<MethodDocComment>(node),
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString(),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses)       
            };

            SetMethodModifiers(newMethod, node.Modifiers);

            _currentParent.AddChild(newMethod);

            base.VisitMethodDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var newProperty = new PropertyModel
            {
                Name = node.Identifier.Text,
                TypeName = node.Type.ToString(),
                DocComment = GetCommentFromNode<PropertyDocComment>(node)
            };
            
            SetPropertyModifiers(newProperty, node.Modifiers);
            SetAccessorProperties(newProperty, node.AccessorList.Accessors);

            _currentParent.AddChild(newProperty);

            base.VisitPropertyDeclaration(node);
        }

        #endregion

        #region Helper Methods


        private bool MergeExistingBaseInterfaceDeclarations<T>(ref T currentInterface) where T:InterfaceBase
        {
            var otherDeclaration = _currentParent[currentInterface.UniqueName];
            if(otherDeclaration == null) return false;

            T otherT = otherDeclaration as T;
            if(otherT == null)
                throw new NamingCollisionException(currentInterface.UniqueName);
                
            if(!otherT.IsPartial)
                throw new NamingCollisionException(currentInterface.UniqueName);

            otherT.InheritanceList = otherT.InheritanceList.Union(currentInterface.InheritanceList).ToList();
            currentInterface = otherT;
            return true;
        }

        #region Comment Helpers

        private static T GetCommentFromNode<T>(SyntaxNode node) where T : DocComment
        {
            var docCommentTrivia =
                node.GetLeadingTrivia()
                    .Where(
                        t =>
                            t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                            t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia));

            var docComment = docCommentTrivia.FirstOrDefault();
            if(docComment.Kind() == SyntaxKind.None) return null;

            string commentXmlString = StripTripleSlashesFromComment(docComment.ToFullString());
            return DocComment.FromXml<T>(commentXmlString);
        }

        private static string StripTripleSlashesFromComment(string xmlComment)
        {
            Regex tripleSlashRegex = new Regex(@"(\r?\n)?\s?///");
            return tripleSlashRegex.Replace(xmlComment, String.Empty);
        }

        #endregion

        #region Modifier Helpers

        private static AccessModifier GetAccessModifier(SyntaxTokenList modifiers, AccessModifier defaultModifier = AccessModifier.Internal)
        {
            AccessModifier tempModifier = defaultModifier;
            bool internalEncountered = false;
            bool protectedEncountered = false;
            foreach(var modifier in modifiers)
            {
                switch(modifier.Kind())
                {
                    case SyntaxKind.PublicKeyword:
                        return AccessModifier.Public;
                    case SyntaxKind.PrivateKeyword:
                        return AccessModifier.Private;
                    case SyntaxKind.ProtectedKeyword:
                        if (internalEncountered) // If we have already encountered an "internal" keyword, the access modifier must be "internal protected".
                            return AccessModifier.ProtectedInternal;
                        tempModifier = AccessModifier.Protected;
                        protectedEncountered = true;
                        break;
                    case SyntaxKind.InternalKeyword:
                        if (protectedEncountered) // If we have already encountered a "protected" keyword, the access modifier must be "internal protected".
                            return AccessModifier.ProtectedInternal;
                        tempModifier = AccessModifier.Internal;
                        internalEncountered = true;
                        break;   
                }
            }
            return tempModifier;
        }

        private static void SetInterfaceBaseModifiers(InterfaceBase interfaceBase, SyntaxTokenList modifiers)
        {
            interfaceBase.AccessModifier = GetAccessModifier(modifiers);
            if(modifiers.Any(m => m.Kind() == SyntaxKind.PartialKeyword))
            {
                interfaceBase.IsPartial = true;
            }
        }

        private static void SetClassModifiers(ClassModel classModel, SyntaxTokenList modifiers)
        {
            SetInterfaceBaseModifiers(classModel, modifiers);
            foreach (var modifier in modifiers)
            {
                switch (modifier.Kind())
                {
                    case SyntaxKind.StaticKeyword:
                        classModel.IsStatic = true;
                        break;
                    case SyntaxKind.AbstractKeyword:
                        classModel.IsAbstract = true;
                        break;
                    case SyntaxKind.SealedKeyword:
                        classModel.IsSealed = true;
                        break;
                }
            }
        }

        private static void SetConstructorModifiers(ConstructorModel constructorModel, SyntaxTokenList modifiers)
        {
            constructorModel.AccessModifier = GetAccessModifier(modifiers);

            if (modifiers.Any(modifier => modifier.Kind() == SyntaxKind.StaticKeyword))
            {
                constructorModel.IsStatic = true;
            }
        }

        private static void SetMethodModifiers(MethodModel method, SyntaxTokenList modifiers)
        {
            method.AccessModifier = GetAccessModifier(modifiers);
            foreach (var modifier in modifiers)
            {
                switch (modifier.Kind())
                {
                    case SyntaxKind.StaticKeyword:
                        method.IsStatic = true;
                        break;
                    case SyntaxKind.AbstractKeyword:
                        method.IsAbstract = true;
                        break;
                    case SyntaxKind.AsyncKeyword:
                        method.IsAsync = true;
                        break;
                    case SyntaxKind.OverrideKeyword:
                        method.IsOverride = true;
                        break;
                    case SyntaxKind.VirtualKeyword:
                        method.IsVirtual = true;
                        break;
                    case SyntaxKind.NewKeyword:
                        method.HidesBaseImplementation = true;
                        break;
                    case SyntaxKind.SealedKeyword:
                        method.IsSealed = true;
                        break;
                }
            }
        }

        private static void SetPropertyModifiers(PropertyModel property, SyntaxTokenList modifiers)
        {
            property.AccessModifier = GetAccessModifier(modifiers);
            foreach (var modifier in modifiers)
            {
                switch (modifier.Kind())
                {
                    case SyntaxKind.StaticKeyword:
                        property.IsStatic = true;
                        break;
                    case SyntaxKind.AbstractKeyword:
                        property.IsAbstract = true;
                        break;
                    case SyntaxKind.OverrideKeyword:
                        property.IsOverride = true;
                        break;
                    case SyntaxKind.VirtualKeyword:
                        property.IsVirtual = true;
                        break;
                    case SyntaxKind.NewKeyword:
                        property.HidesBaseImplementation = true;
                        break;
                    case SyntaxKind.SealedKeyword:
                        property.IsSealed = true;
                        break;
                }
            }
        }

        #endregion

        #region Parameter Helpers

        private static IList<TypeParameterModel> GetTypeParameterList(TypeParameterListSyntax typeParamList, SyntaxList<TypeParameterConstraintClauseSyntax> constraints)
        {
            if(typeParamList == null) return new List<TypeParameterModel>();
            var paramConstraintMap = constraints.ToDictionary(constraint => constraint.Name.ToString(), constraint => constraint.Constraints.ToString());
            return typeParamList.Parameters.Select(typeParam => typeParam.Identifier.ToString()).Select(paramName => new TypeParameterModel
            {
                Name = paramName, 
                Constraint = paramConstraintMap.ContainsKey(paramName) ? paramConstraintMap[paramName] : null
            }).ToList();
        }

        private static IList<ParameterModel> GetParameterList(SeparatedSyntaxList<ParameterSyntax> parameterSyntaxList)
        {
            return parameterSyntaxList.Select(paramSyntax => new ParameterModel
            {
                Name = paramSyntax.Identifier.Text, 
                TypeName = paramSyntax.Type.ToString(), 
                ParameterKind = GetParameterKindFromSyntax(paramSyntax)
            }).ToList();
        }

        private static ParameterKind GetParameterKindFromSyntax(ParameterSyntax param)
        {
            if(param.Modifiers.Any())
            {
                switch(param.Modifiers[0].Kind())
                {
                    case SyntaxKind.RefKeyword:
                        return ParameterKind.Ref;
                    case SyntaxKind.OutKeyword:
                        return ParameterKind.Out;
                    case SyntaxKind.ParamsKeyword:
                        return ParameterKind.Params;
                }  
            }
            return ParameterKind.Value;
        }

        #endregion

        private static void SetAccessorProperties(PropertyModel property, SyntaxList<AccessorDeclarationSyntax> accessors)
        {
            foreach(var accessor in accessors)
            {
                if(accessor.Kind() == SyntaxKind.GetAccessorDeclaration)
                {
                    property.HasGetter = true;
                    property.GetterAccessModifier = GetAccessModifier(accessor.Modifiers, property.AccessModifier);
                }
                else if(accessor.Kind() == SyntaxKind.SetAccessorDeclaration)
                {
                    property.HasSetter = true;
                    property.SetterAccessModifier = GetAccessModifier(accessor.Modifiers, property.AccessModifier);
                }
            }
        }

        private static IList<string> GetInheritanceList(BaseListSyntax baseList)
        {
            if(baseList == null) return new List<string>();
            return baseList.Types.Select(t => t.ToString()).ToList();
        }

        #endregion
    }
}
