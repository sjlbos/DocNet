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
        public NamespaceModel GlobalNamespace { get; private set; }

        private NamespaceModel _currentNamespace;
        private InterfaceModel _currentInterface;

        public CsCommentWalker(NamespaceModel globalNamespace)
        {
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            GlobalNamespace = globalNamespace;
        }

        #region Node Processors

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

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
            if(node == null) throw new ArgumentNullException("node");

            var newInterface = new InterfaceModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node),
                TypeParameters = GetTypeParameterList(node.TypeParameterList.Parameters, node.ConstraintClauses)
            };

            SetInheritanceList(newInterface, node.BaseList);

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
            if (node == null) throw new ArgumentNullException("node");

            var newClass = new ClassModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };

            if(node.TypeParameterList != null)
            {
                newClass.TypeParameters = GetTypeParameterList(node.TypeParameterList.Parameters, node.ConstraintClauses);
            }

            SetInheritanceList(newClass, node.BaseList);
            SetClassModifiers(newClass, node.Modifiers);

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
            if (node == null) throw new ArgumentNullException("node");

            var newStruct = new StructModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                AccessModifier = GetAccessModifier(node.Modifiers),
                DocComment = GetCommentFromNode<InterfaceDocComment>(node)
            };

            if (node.TypeParameterList != null)
            {
                newStruct.TypeParameters = GetTypeParameterList(node.TypeParameterList.Parameters, node.ConstraintClauses);
            }

            SetInheritanceList(newStruct, node.BaseList);

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
            if (node == null) throw new ArgumentNullException("node");

            var newEnum = new EnumModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                AccessModifier = GetAccessModifier(node.Modifiers),
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
            if (node == null) throw new ArgumentNullException("node");

            var newDelegate = new DelegateModel
            {
                Name = node.Identifier.Text,
                Namespace = _currentNamespace,
                Parent = _currentInterface,
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString(),
                DocComment = GetCommentFromNode<MethodDocComment>(node)
            };

            if (node.TypeParameterList != null)
            {
                newDelegate.TypeParameters = GetTypeParameterList(node.TypeParameterList.Parameters, node.ConstraintClauses);
            }

            if(TypeIsNested())
                LinkTypeToParent(newDelegate);
            else
                _currentNamespace.Delegates.Add(newDelegate);

            base.VisitDelegateDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newConstructor = new ConstructorModel
            {
                Name = node.Identifier.Text,
                Parent = _currentInterface as ClassAndStructModel,
                DocComment = GetCommentFromNode<MethodDocComment>(node),
                Parameters = GetParameterList(node.ParameterList.Parameters)
            };

            SetConstructorModifiers(newConstructor, node.Modifiers); 

            ((ClassAndStructModel) _currentInterface).Constructors.Add(newConstructor);

            base.VisitConstructorDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var newMethod = new MethodModel
            {
                Name = node.Identifier.Text,
                Parent = _currentInterface,
                DocComment = GetCommentFromNode<MethodDocComment>(node),
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString()
            };

            if(node.TypeParameterList != null)
            {
                newMethod.TypeParameters = GetTypeParameterList(node.TypeParameterList.Parameters, node.ConstraintClauses);
            }

            SetMethodModifiers(newMethod, node.Modifiers);

            _currentInterface.Methods.Add(newMethod);

            base.VisitMethodDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var newProperty = new PropertyModel
            {
                Name = node.Identifier.Text,
                TypeName = node.Type.ToString(),
                Parent = _currentInterface,
                DocComment = GetCommentFromNode<PropertyDocComment>(node)
            };
            
            SetPropertyModifiers(newProperty, node.Modifiers);
            SetAccessorProperties(newProperty, node.AccessorList.Accessors);

            _currentInterface.Properties.Add(newProperty);

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

        private static void SetClassModifiers(ClassModel classModel, SyntaxTokenList modifiers)
        {
            classModel.AccessModifier = GetAccessModifier(modifiers);
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

        private static IList<TypeParameterModel> GetTypeParameterList(SeparatedSyntaxList<TypeParameterSyntax> typeParams, SyntaxList<TypeParameterConstraintClauseSyntax> constraints)
        {
            var paramConstraintMap = constraints.ToDictionary(constraint => constraint.Name.ToString(), constraint => constraint.Constraints.ToString());
            return typeParams.Select(typeParam => typeParam.Identifier.ToString()).Select(paramName => new TypeParameterModel
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

        private static void SetInheritanceList(InterfaceModel interfaceModel, BaseListSyntax baseList)
        {
            if(baseList == null) return;
            foreach(var type in baseList.Types)
            {
                interfaceModel.InheritanceList.Add(type.ToString());
            }
        }


        #endregion
    }
}
