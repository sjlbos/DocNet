using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Output.Html.Helpers
{
    /// <summary>
    /// This class provides helper methods for converting DocNet C# models into their corresponding declaration strings.
    /// </summary>
    /// <remarks>
    /// This class exists in order to ensure generated documentation maintains consistent formatting and modifier order when 
    /// displaying the declaration strings for classes, methods, etc. The declaration cannot be read directly from the source code,
    /// as the programmer may use different modifier orders accross different classes and element types.
    /// </remarks>
    public static class GetDeclaration
    {
        /// <summary>
        /// Gets the declaration string of the provided InterfaceModel.
        /// </summary>
        /// <param name="interfaceModel">The InterfaceModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form <c>[access modifier] interface [name]</c> 
        /// or an empty string if the provided InterfaceModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfInterface(InterfaceModel interfaceModel)
        {
            if (interfaceModel == null || String.IsNullOrWhiteSpace(interfaceModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            sb.Append(GetAccessModifierString(interfaceModel.AccessModifier));
            sb.Append(" interface ");
            sb.Append(interfaceModel.Name);
            if (interfaceModel.TypeParameters.Any())
            {
                AppendGenericsList(sb, interfaceModel.TypeParameters);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided ClassModel.
        /// </summary>
        /// <param name="classModel">The ClassModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form 
        /// <c>[access modifier] [static|abstract|sealed] class [name][generic types][:base class, interface list]</c>
        /// or an empty string if the provided ClassModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfClass(ClassModel classModel)
        {
            if (classModel == null || String.IsNullOrWhiteSpace(classModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            sb.Append(GetAccessModifierString(classModel.AccessModifier));
            if (classModel.IsStatic)
            {
                sb.Append(" static");
            }
            else if (classModel.IsAbstract)
            {
                sb.Append(" abstract");
            }
            else if (classModel.IsSealed)
            {
                sb.Append(" sealed");
            }
            sb.Append(" class ");
            sb.Append(classModel.Name);
            if (classModel.TypeParameters.Any())
            {
                AppendGenericsList(sb, classModel.TypeParameters);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided StructModel.
        /// </summary>
        /// <param name="structModel">The StructModel to get the declaration of.</param>
        /// <returns>
        ///  A string of the form <c>[access modifier] struct [name][generic types][:interface list]</c>
        /// or an empty string if the provided StructModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfStruct(StructModel structModel)
        {
            if (structModel == null || String.IsNullOrWhiteSpace(structModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            sb.Append(GetAccessModifierString(structModel.AccessModifier));
            sb.Append(" struct ");
            sb.Append(structModel.Name);
            if (structModel.TypeParameters.Any())
            {
                AppendGenericsList(sb, structModel.TypeParameters);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided EnumModel.
        /// </summary>
        /// <param name="enumModel">The EnumModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form <c>[access modifier] enum [name]</c> 
        /// or an empty string if the provided EnumModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfEnum(EnumModel enumModel)
        {
            if (enumModel == null || String.IsNullOrWhiteSpace(enumModel.Name))
                return String.Empty;

            return String.Format(CultureInfo.InvariantCulture,
                "{0} enum {1}", 
                GetAccessModifierString(enumModel.AccessModifier),
                enumModel.Name);
        }

        /// <summary>
        /// Gets the declaration string of the provided DelegateModel.
        /// </summary>
        /// <param name="delegateModel">The DelegateModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form <c>[access modifier] delegate [return type] [name][generic types][parameters]</c> 
        /// or an empty string if the provided DelegateModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfDelegate(DelegateModel delegateModel)
        {
            if (delegateModel == null || String.IsNullOrWhiteSpace(delegateModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            sb.Append(GetAccessModifierString(delegateModel.AccessModifier));
            sb.Append(" delegate ");
            sb.Append(delegateModel.ReturnType);
            sb.Append(" ");
            sb.Append(delegateModel.Name);
            AppendGenericsList(sb, delegateModel.TypeParameters);
            AppendParameterList(sb, delegateModel.Parameters);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided ConstructorModel.
        /// </summary>
        /// <param name="constructorModel">The ConstructorModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form <c>[access modifier] [static] [name][parameters]</c> 
        /// or an empty string if the provided ConstructorModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfConstructor(ConstructorModel constructorModel)
        {
            if (constructorModel == null || String.IsNullOrWhiteSpace(constructorModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            sb.Append(GetAccessModifierString(constructorModel.AccessModifier));
            if (constructorModel.IsStatic)
            {
                sb.Append(" static");
            }
            sb.Append(" ");
            sb.Append(constructorModel.Name);
            AppendParameterList(sb, constructorModel.Parameters);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided MethodModel.
        /// </summary>
        /// <param name="methodModel">The MethodModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form 
        /// <c>[new] [access modifier] [static|virtual|abstract|sealed|override] [async] [return type] [name][generic types][parameters]</c> 
        /// or an empty string if the provided MethodModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfMethod(MethodModel methodModel)
        {
            if (methodModel == null || String.IsNullOrWhiteSpace(methodModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            if (methodModel.HidesBaseImplementation)
                sb.Append("new ");

            sb.Append(GetAccessModifierString(methodModel.AccessModifier));

            if (methodModel.IsStatic)
                sb.Append(" static");

            if (methodModel.IsOverride)
                sb.Append(" override");

            if (methodModel.IsAbstract)
                sb.Append(" abstract");

            if (methodModel.IsVirtual)
                sb.Append(" virtual");

            if (methodModel.IsSealed)
                sb.Append(" sealed");

            if (methodModel.IsAsync)
                sb.Append(" async");

            sb.Append(" ");
            sb.Append(methodModel.ReturnType);
            sb.Append(" ");
            sb.Append(methodModel.Name);
            AppendGenericsList(sb, methodModel.TypeParameters);
            AppendParameterList(sb, methodModel.Parameters);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the declaration string of the provided PropertyModel.
        /// </summary>
        /// <param name="propertyModel">The PropertyModel to get the declaration of.</param>
        /// <returns>
        /// A string of the form 
        /// <c>[new] [access modifier] [static|virtual|abstract|sealed|override] [type] [name] [accessor list]</c> 
        /// or an empty string if the provided PropertyModel is null or has a null or empty "Name" property.
        /// </returns>
        public static string OfProperty(PropertyModel propertyModel)
        {
            if (propertyModel == null || String.IsNullOrWhiteSpace(propertyModel.Name))
                return String.Empty;

            var sb = new StringBuilder();
            if (propertyModel.HidesBaseImplementation)
                sb.Append("new ");

            sb.Append(GetAccessModifierString(propertyModel.AccessModifier));

            if (propertyModel.IsStatic)
                sb.Append(" static");

            if (propertyModel.IsOverride)
                sb.Append(" override");

            if (propertyModel.IsAbstract)
                sb.Append(" abstract");

            if (propertyModel.IsVirtual)
                sb.Append(" virtual");

            if (propertyModel.IsSealed)
                sb.Append(" sealed");

            sb.Append(" ");
            sb.Append(propertyModel.TypeName);
            sb.Append(" ");
            sb.Append(propertyModel.Name);
            sb.Append(" { ");

            if (propertyModel.HasGetter)
            {
                if (propertyModel.AccessModifier != propertyModel.GetterAccessModifier)
                {
                    sb.Append(GetAccessModifierString(propertyModel.GetterAccessModifier));
                    sb.Append(" ");
                }    
                sb.Append("get; ");
            }

            if (propertyModel.HasSetter)
            {
                if (propertyModel.AccessModifier != propertyModel.SetterAccessModifier)
                {
                    sb.Append(GetAccessModifierString(propertyModel.SetterAccessModifier));
                    sb.Append(" ");
                }
                sb.Append("set; ");
            }

            sb.Append("}");

            return sb.ToString();
        }

        #region Helper Methods

        private static string GetAccessModifierString(AccessModifier accessModifier)
        {
            switch (accessModifier)
            {
                case AccessModifier.Public:
                    return "public";
                case AccessModifier.Private:
                    return "private";
                case AccessModifier.Protected:
                    return "protected";
                case AccessModifier.Internal:
                    return "internal";
                case AccessModifier.ProtectedInternal:
                    return "protected internal";
            }
            throw new InvalidEnumArgumentException(accessModifier.ToString());
        }

        private static void AppendGenericsList(StringBuilder sb, IEnumerable<TypeParameterModel> typeParameters)
        {
            if (!typeParameters.Any()) return;
            sb.Append("<");
            sb.Append(String.Join(", ", typeParameters.Select(t => t.Name)));
            sb.Append(">");
        }

        private static void AppendParameterList(StringBuilder sb, IEnumerable<ParameterModel> parameterList)
        {
            sb.Append("(");
            sb.Append(String.Join(", ", parameterList.Select(p => p.TypeName + " " + p.Name)));
            sb.Append(")");
        }

        #endregion
    }
}
