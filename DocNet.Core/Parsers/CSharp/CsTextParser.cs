using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocNet.Core.Exceptions;
using DocNet.Core.Models.Comments;
using DocNet.Core.Models.CSharp;
using log4net;
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
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        public GlobalNamespaceModel GetGlobalNamespace(string sourceCode, OutputMode outputMode)
        {
            if(sourceCode == null)
                throw new ArgumentNullException("sourceCode");
            var sourceText = SourceText.From(sourceCode);
            return GetGlobalNamespace(sourceText, outputMode);
        }

        /// <summary>
        /// Parses C# source code and adds the parsed elements to the specified namespace.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <param name="globalNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        public void ParseIntoNamespace(string sourceCode, GlobalNamespaceModel globalNamespace, OutputMode outputMode)
        {
            if(sourceCode == null)
                throw new ArgumentNullException("sourceCode");
            if(globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            var sourceText = SourceText.From(sourceCode);
            ParseIntoNamespace(sourceText, globalNamespace, outputMode);
        }

        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        public GlobalNamespaceModel GetGlobalNamespace(Stream sourceFileStream, OutputMode outputMode)
        {
            if(sourceFileStream == null)
                throw new ArgumentNullException("sourceFileStream");
            var sourceText = SourceText.From(sourceFileStream);
            return GetGlobalNamespace(sourceText, outputMode);
        }

        /// <summary>
        /// Parses C# source code and adds the parsed elments to the specified namespace.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <param name="globalNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        public void ParseIntoNamespace(FileStream sourceFileStream, GlobalNamespaceModel globalNamespace, OutputMode outputMode)
        {
            if (sourceFileStream == null)
                throw new ArgumentNullException("sourceFileStream");
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            var sourceText = SourceText.From(sourceFileStream);
            ParseIntoNamespace(sourceText, globalNamespace, outputMode);
        }

        private static GlobalNamespaceModel GetGlobalNamespace(SourceText csText, OutputMode outputMode)
        {
            var namespaceModel = new GlobalNamespaceModel();
            ParseIntoNamespace(csText, namespaceModel, outputMode);
            return namespaceModel;
        }

        private static void ParseIntoNamespace(SourceText csText, GlobalNamespaceModel namespaceModel, OutputMode outputMode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(csText);
            var walker = new CsCommentWalker(namespaceModel, outputMode);
            walker.Visit(tree.GetRoot());
        }
    }



    internal class CsCommentWalker : CSharpSyntaxWalker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (CsCommentWalker));

        private IParentElement _currentParent;
        private readonly OutputMode _outputMode;

        public CsCommentWalker(GlobalNamespaceModel globalNamespace, OutputMode outputMode)
        {
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            _currentParent = globalNamespace;
            _outputMode = outputMode;
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
                currentNamespace = new NamespaceModel { Identifier = namespaceName }; 
            }
            // Otherwise, use the existing namespace
            else
            {
                if(!(_currentParent[namespaceName] is NamespaceModel)) 
                    throw new NamingCollisionException(namespaceName);

                currentNamespace = _currentParent[namespaceName] as NamespaceModel;
            }     

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentNamespace;

            base.VisitNamespaceDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;

            if (_currentParent[namespaceName] == null && NamespaceShouldBeAdded(currentNamespace))
            {
                _currentParent.AddChild(currentNamespace);
            }
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            var currentInterface = new InterfaceModel
            {
                Identifier = node.Identifier.Text,
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList),
                DocComment = GetCommentFromNode<InterfaceDocComment>(node, node.Identifier.Text)
            };
            SetInterfaceBaseModifiers(currentInterface, node.Modifiers);

            bool interfaceWasMerged = false;
            if (currentInterface.IsPartial)
            {
                interfaceWasMerged = MergeExistingBaseInterfaceDeclarations(ref currentInterface);
            }

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentInterface;
       
            base.VisitInterfaceDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;

            if (TypeShouldBeAdded(currentInterface) && !interfaceWasMerged)
            {
                _currentParent.AddChild(currentInterface);
            }
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var currentClass = new ClassModel
            {
                Identifier = node.Identifier.Text,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node, node.Identifier.Text),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList)
            };
            SetClassModifiers(currentClass, node.Modifiers);

            bool classWasMerged = false;
            if (currentClass.IsPartial)
            {
                classWasMerged = MergeExistingBaseInterfaceDeclarations(ref currentClass);
            }

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentClass;
             
            base.VisitClassDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;

            if (TypeShouldBeAdded(currentClass) && !classWasMerged)
            {
                _currentParent.AddChild(currentClass);
            }
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var currentStruct = new StructModel
            {
                Identifier = node.Identifier.Text,
                DocComment = GetCommentFromNode<InterfaceDocComment>(node, node.Identifier.Text),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                InheritanceList = GetInheritanceList(node.BaseList)
            };
            SetInterfaceBaseModifiers(currentStruct, node.Modifiers);

            bool structWasMerged = false;
            if (currentStruct.IsPartial)
            {
                structWasMerged = MergeExistingBaseInterfaceDeclarations(ref currentStruct);
            }

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = currentStruct;

            base.VisitStructDeclaration(node);

            // Restore old parent
            _currentParent = oldParent;

            if (TypeShouldBeAdded(currentStruct) && !structWasMerged)
            {
                _currentParent.AddChild(currentStruct);
            }
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newEnum = new EnumModel
            {
                Identifier = node.Identifier.Text,
                AccessModifier = GetAccessModifier(node.Modifiers),
                DocComment = GetCommentFromNode<DocComment>(node, node.Identifier.Text),
                Fields = node.Members.Select(m => m.Identifier.Text).ToList()
            };

            if (TypeShouldBeAdded(newEnum))
            {
                _currentParent.AddChild(newEnum);
                base.VisitEnumDeclaration(node);
            }
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newDelegate = new DelegateModel
            {
                Identifier = node.Identifier.Text,
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString(),
                DocComment = GetCommentFromNode<MethodDocComment>(node, node.Identifier.Text),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses),
                AccessModifier = GetAccessModifier(node.Modifiers)
            };

            if (TypeShouldBeAdded(newDelegate))
            {
                _currentParent.AddChild(newDelegate);
                base.VisitDelegateDeclaration(node);
            } 
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (node == null) throw new ArgumentNullException("node");

            var newConstructor = new ConstructorModel
            {
                Identifier = node.Identifier.Text,
                DocComment = GetCommentFromNode<MethodDocComment>(node, node.Identifier.Text),
                Parameters = GetParameterList(node.ParameterList.Parameters)
            };

            SetConstructorModifiers(newConstructor, node.Modifiers); 

            if(ConstructorShouldBeAdded(newConstructor))
            {
                _currentParent.AddChild(newConstructor);
                base.VisitConstructorDeclaration(node);   
            }
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            string methodName = (node.ExplicitInterfaceSpecifier != null)
                ? node.ExplicitInterfaceSpecifier + node.Identifier.Text
                : node.Identifier.Text;

            var newMethod = new MethodModel
            {
                Identifier = methodName,
                DocComment = GetCommentFromNode<MethodDocComment>(node, node.Identifier.Text),
                Parameters = GetParameterList(node.ParameterList.Parameters),
                ReturnType = node.ReturnType.ToString(),
                TypeParameters = GetTypeParameterList(node.TypeParameterList, node.ConstraintClauses)       
            };

            SetMethodModifiers(newMethod, node.Modifiers);

            if (MethodShouldBeAdded(newMethod))
            {
                _currentParent.AddChild(newMethod);
                base.VisitMethodDeclaration(node);
            }
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if(node == null) throw new ArgumentNullException("node");

            string propertyName = (node.ExplicitInterfaceSpecifier != null)
                ? node.ExplicitInterfaceSpecifier + "." + node.Identifier.Text
                : node.Identifier.Text;

            var newProperty = new PropertyModel
            {
                Identifier = propertyName,
                TypeName = node.Type.ToString(),
                DocComment = GetCommentFromNode<PropertyDocComment>(node, node.Identifier.Text)
            };
            
            SetPropertyModifiers(newProperty, node.Modifiers);
            SetAccessorProperties(newProperty, node.AccessorList.Accessors);

            if (PropertyShouldBeAdded(newProperty))
            {
                _currentParent.AddChild(newProperty);
                base.VisitPropertyDeclaration(node);
            }
        }

        #endregion

        #region Helper Methods

        private bool MergeExistingBaseInterfaceDeclarations<T>(ref T currentInterface) where T:InterfaceBase
        {
            var otherDeclaration = _currentParent[currentInterface.InternalName];
            if(otherDeclaration == null) return false;

            T otherT = otherDeclaration as T;
            if(otherT == null)
                throw new NamingCollisionException(currentInterface.InternalName);
                
            if(!otherT.IsPartial)
                throw new NamingCollisionException(currentInterface.InternalName);

            otherT.InheritanceList = otherT.InheritanceList.Union(currentInterface.InheritanceList).ToList();
            currentInterface = otherT;
            return true;
        }

        private bool NamespaceShouldBeAdded(NamespaceModel namespaceModel)
        {
            return namespaceModel.Any();
        }

        private bool TypeShouldBeAdded(CsType csType)
        {
            switch (_outputMode)
            {
                case OutputMode.AllElements:
                    return true;
                case OutputMode.PublicOnly:
                    return csType.AccessModifier == AccessModifier.Public;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool MethodShouldBeAdded(MethodModel method)
        {
            switch (_outputMode)
            {
                case OutputMode.AllElements:
                    return true;
                case OutputMode.PublicOnly:
                    return method.AccessModifier == AccessModifier.Public;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool ConstructorShouldBeAdded(ConstructorModel constructorModel)
        {
            switch (_outputMode)
            {
                case OutputMode.AllElements:
                    return true;
                case OutputMode.PublicOnly:
                    return constructorModel.AccessModifier == AccessModifier.Public;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool PropertyShouldBeAdded(PropertyModel property)
        {
            switch (_outputMode)
            {
                case OutputMode.AllElements:
                    return true;
                case OutputMode.PublicOnly:
                    return property.AccessModifier == AccessModifier.Public;      
                default:
                    throw new NotImplementedException();
            }
        }

        #region Comment Helpers

        private static T GetCommentFromNode<T>(SyntaxNode node, string nodeName) where T : DocComment
        {
            var docCommentTrivia =
                node.GetLeadingTrivia()
                    .Where(
                        t =>
                            t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia) ||
                            t.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia));

            var docCommentText = docCommentTrivia.FirstOrDefault();
            if(docCommentText.Kind() == SyntaxKind.None) return null;

            string commentXmlString = StripTripleSlashesFromComment(docCommentText.ToFullString());
            try
            {
                return DocComment.FromXml<T>(commentXmlString);
            }
            catch (InvalidOperationException ex)
            {
                Log.Debug(ex);
                Log.WarnFormat("The documentation comment on element \"{0}\" contains invalid XML and cannot be processed.", nodeName);
                return null;
            }
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
