
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using DocNet.Core.Models.Comments.Xml;
using DocNet.Core.Models.CSharp;
using Assembly = System.Reflection.Assembly;

namespace DocNet.Core.Parsers.CSharp
{
    public class CsAssemblyParser : ICsAssemblyParser
    {
        public GlobalNamespaceModel GetGlobalNamespace(Assembly assembly, string docFileXml, OutputMode outputMode)
        {
            var globalNamespace = new GlobalNamespaceModel();
            ParseIntoNamespace(assembly, docFileXml, globalNamespace, outputMode);
            return globalNamespace;
        }

        public void ParseIntoNamespace(Assembly assembly, string docFileXml, GlobalNamespaceModel globalNamespace, OutputMode outputMode)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (docFileXml == null)
                throw new ArgumentNullException("docFileXml");
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");

            var docFileModel = DeserializeDocFileXmlString(docFileXml);

            var walker = new CsAssemblyWalker(globalNamespace, assembly, docFileModel, outputMode);
            walker.VisitAssembly();
        }

        public GlobalNamespaceModel GetGlobalNamespace(Assembly assembly, FileStream docFile, OutputMode outputMode)
        {
            var globalNamespace = new GlobalNamespaceModel();
            ParseIntoNamespace(assembly, docFile, globalNamespace, outputMode);
            return globalNamespace;
        }

        public void ParseIntoNamespace(Assembly assembly, FileStream docFile, GlobalNamespaceModel globalNamespace,
            OutputMode outputMode)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            if (docFile == null)
                throw new ArgumentNullException("docFile");
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");

            var docFileModel = DeserializeDocFileXmlString(docFile);

            var walker = new CsAssemblyWalker(globalNamespace, assembly, docFileModel, outputMode);
            walker.VisitAssembly();
        }

        #region Helper Methods

        private static DocFileModel DeserializeDocFileXmlString(string docFileXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DocFileModel));
            using (var reader = new XmlTextReader(new StringReader(docFileXml)))
            {
                if (serializer.CanDeserialize(reader))
                {
                    return serializer.Deserialize(reader) as DocFileModel;
                }
            }
            return null;
        }

        private static DocFileModel DeserializeDocFileXmlString(Stream xmlDocFile)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DocFileModel));
            using (var reader = new XmlTextReader(new StreamReader(xmlDocFile)))
            {
                if (serializer.CanDeserialize(reader))
                {
                    return serializer.Deserialize(reader) as DocFileModel;
                }
            }
            return null;
        }

        #endregion
    }

    internal class CsAssemblyWalker : AssemblyWalker
    {
        private GlobalNamespaceModel _globalNamespace;
        private readonly OutputMode _outputMode;
        private readonly DocFileModel _docFileModel;
        private IParentElement _currentParent;

        public CsAssemblyWalker(GlobalNamespaceModel globalNamespace, Assembly assembly, DocFileModel docFileModel, OutputMode outputMode)
            : base(assembly)
        {
            if (globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            if (docFileModel == null)
                throw new ArgumentNullException("docFileModel");

            _globalNamespace = globalNamespace;
            _currentParent = globalNamespace;
            _docFileModel = docFileModel;
            _outputMode = outputMode;
        }

        #region Visitors

        public override void VisitNamespace(string namespaceName)
        {
            if (namespaceName == null)
                throw new ArgumentNullException("namespaceName");

            var namespaceModel = new NamespaceModel
            {
                Identifier = namespaceName
            };

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = namespaceModel;

            base.VisitNamespace(namespaceName);

            // Restore old parent
            _currentParent = oldParent;

            _currentParent.AddChild(namespaceModel);
        }

        public override void VisitClass(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            var classModel = new ClassModel
            {
                Identifier = t.Name,
                TypeParameters = GetTypeParameterList(t),
                IsAbstract = t.IsAbstract,
                IsSealed = t.IsSealed,
                IsStatic = t.IsSealed && t.IsAbstract, // Static classes are internally just sealed abstract classes
                AccessModifier = GetAccessModifierForType(t),
                InheritanceList = GetInheritanceListForType(t)
            };

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = classModel;

            base.VisitClass(t);

            // Restore old parent
            _currentParent = oldParent;

            if (InterfaceTypeShouldBeAdded(classModel))
            {
                _currentParent.AddChild(classModel);
            }
        }

        public override void VisitInterface(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            var interfaceModel = new InterfaceModel
            {
                Identifier = t.Name,
                TypeParameters = GetTypeParameterList(t),
                AccessModifier = GetAccessModifierForType(t),
                InheritanceList = GetInheritanceListForType(t)
            };

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = interfaceModel;

            base.VisitInterface(t);

            // Restore old parent
            _currentParent = oldParent;

            if (InterfaceTypeShouldBeAdded(interfaceModel))
            {
                _currentParent.AddChild(interfaceModel);
            }
        }

        public override void VisitStruct(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            var structModel = new StructModel
            {
                Identifier = t.Name,
                TypeParameters = GetTypeParameterList(t),
                AccessModifier = GetAccessModifierForType(t),
                InheritanceList = GetInheritanceListForType(t)
            };

            // Update current parent
            var oldParent = _currentParent;
            _currentParent = structModel;

            base.VisitStruct(t);

            // Restore old parent
            _currentParent = oldParent;

            if (InterfaceTypeShouldBeAdded(structModel))
            {
                _currentParent.AddChild(structModel);
            }
        }

        public override void VisitEnum(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            var enumModel = new EnumModel
            {
                Identifier = t.Name,
                AccessModifier = GetAccessModifierForType(t),
                Fields = t.GetEnumNames()
            };

            base.VisitEnum(t);

            if (NestedTypeShouldBeAdded(enumModel.AccessModifier, enumModel.DocComment == null))
            {
                _currentParent.AddChild(enumModel);
            }
        }

        public override void VisitDelegate(Type t)
        {
            if (t == null)
                throw new ArgumentNullException("t");

            var delegateModel = new DelegateModel
            {
                Identifier = t.Name,
                TypeParameters = GetTypeParameterList(t),
                AccessModifier = GetAccessModifierForType(t)
            };

            base.VisitDelegate(t);

            if (NestedTypeShouldBeAdded(delegateModel.AccessModifier, delegateModel.DocComment == null))
            {
                _currentParent.AddChild(delegateModel);
            }
        }

        public override void VisitConstructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null)
                throw new ArgumentNullException("constructorInfo");

            var constructorModel = new ConstructorModel
            {
                Identifier = constructorInfo.Name,
                IsStatic = constructorInfo.IsStatic,
            };

            base.VisitConstructor(constructorInfo);

            if (NestedTypeShouldBeAdded(constructorModel.AccessModifier, constructorModel.DocComment == null))
            {
                _currentParent.AddChild(constructorModel);
            }
        }

        public override void VisitMethod(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException("methodInfo");

            var methodModel = new MethodModel
            {
                Identifier = methodInfo.Name
            };

            base.VisitMethod(methodInfo);

            if (NestedTypeShouldBeAdded(methodModel.AccessModifier, methodModel.DocComment == null))
            {
                _currentParent.AddChild(methodModel);
            }
        }

        public override void VisitProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            var propertyModel = new PropertyModel
            {
                Identifier = propertyInfo.Name
            };

            base.VisitProperty(propertyInfo);

            if (NestedTypeShouldBeAdded(propertyModel.AccessModifier, propertyModel.DocComment == null))
            {
                _currentParent.AddChild(propertyModel);
            }
        }

        public override void VisitField(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
                throw new ArgumentNullException("fieldInfo");

            base.VisitField(fieldInfo);
        }

        public override void VisitEvent(EventInfo eventInfo)
        {
            if (eventInfo == null)
                throw new ArgumentNullException("eventInfo");

            base.VisitEvent(eventInfo);
        }

        #endregion

        #region Helper Methods

        #region Doc Comments


        #endregion

        #region Modifiers

        // See https://msdn.microsoft.com/en-us/library/ms173183.aspx?f=255&MSPPError=-2147217396 
        // for more info on access modifiers in IL
        private static AccessModifier GetAccessModifierForType(Type t)
        {
            if (t.IsPublic)
                return AccessModifier.Public;

            if (t.IsNestedFamORAssem)
                return AccessModifier.ProtectedInternal;

            // ToDo: Determine how to identify internal classes

            return AccessModifier.Internal;
        }

        private static AccessModifier GetAccessModifierForConstructor(ConstructorInfo constructorInfo)
        {
            if (constructorInfo.IsPrivate)
                return AccessModifier.Public;
            if (constructorInfo.IsPrivate)
                return AccessModifier.Private;
            if (constructorInfo.IsFamilyOrAssembly)
                return AccessModifier.ProtectedInternal;
            if (constructorInfo.IsFamily)
                return AccessModifier.Protected;
            return AccessModifier.Internal;
        }

        private static AccessModifier GetAccessModifierForMethod(MethodInfo methodInfo)
        {
            if (methodInfo.IsPrivate)
                return AccessModifier.Public;
            if (methodInfo.IsPrivate)
                return AccessModifier.Private;
            if (methodInfo.IsFamilyOrAssembly)
                return AccessModifier.ProtectedInternal;
            if (methodInfo.IsFamily)
                return AccessModifier.Protected;
            return AccessModifier.Internal;
        }

        #endregion

        #region Parameters

        private static IList<TypeParameterModel> GetTypeParameterList(Type t)
        {
            return t.GenericTypeArguments.Select(typeParam => new TypeParameterModel
            {
                Name = typeParam.Name,
                Constraint = GetGenericParameterConstraintForType(typeParam)
            }).ToList();
        }

        private static string GetGenericParameterConstraintForType(Type t)
        {
            var genericParameterAttributes = t.GenericParameterAttributes;
            var specialConstraints = genericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
            if (specialConstraints != GenericParameterAttributes.None)
            {
                if ((specialConstraints & GenericParameterAttributes.ReferenceTypeConstraint) != 0)
                {

                }
                if ((specialConstraints & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0)
                {

                }
                if ((specialConstraints & GenericParameterAttributes.DefaultConstructorConstraint) != 0)
                {

                }
            }

            var typeConstraints = t.GetGenericParameterConstraints();
            if (typeConstraints.Any())
            {

            }
            return null;
        }

        #endregion

        private static IList<string> GetInheritanceListForType(Type t)
        {
            var inheritanceList = new List<string>();
            if (t.BaseType != null && t.BaseType != typeof(object))
            {
                inheritanceList.Add(t.BaseType.Name);
            }
            inheritanceList.AddRange(t.GetInterfaces().Select(i => i.Name).ToList());
            return inheritanceList;
        }

        #region Selection Rules

        private bool InterfaceTypeShouldBeAdded(InterfaceBase interfaceBase)
        {
            // Todo: Should include include undocumented elements with documented children
            switch (_outputMode)
            {
                case OutputMode.PublicOnly:
                    return interfaceBase.AccessModifier == AccessModifier.Public;
                case OutputMode.DocumentedOnly:
                    return interfaceBase.DocComment != null;
                case OutputMode.PublicDocumentedOnly:
                    return interfaceBase.AccessModifier == AccessModifier.Public && interfaceBase.DocComment != null;
                default:
                    return true;
            }
        }

        public bool NestedTypeShouldBeAdded(AccessModifier accessModifier, bool hasDocComment)
        {
            switch (_outputMode)
            {
                case OutputMode.PublicOnly:
                    return accessModifier == AccessModifier.Public;
                case OutputMode.DocumentedOnly:
                    return hasDocComment;
                case OutputMode.PublicDocumentedOnly:
                    return accessModifier == AccessModifier.Public && hasDocComment;
                default:
                    return true;
            }
        }

        #endregion

        #endregion
    }
}
