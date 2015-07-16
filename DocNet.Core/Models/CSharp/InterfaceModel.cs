using System;
using System.Collections.Generic;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class InterfaceModel : CsTypeModel, IEquatable<InterfaceModel>
    {
        public IList<TypeParameterModel> TypeParameters { get; set; } 
        public IList<MethodModel> Methods;
        public IList<PropertyModel> Properties;
        public IList<string> InheritanceList; 
        public InterfaceDocComment DocComment { get; set; }

        public InterfaceModel()
        {
            TypeParameters = new List<TypeParameterModel>();
            Methods = new List<MethodModel>();
            Properties = new List<PropertyModel>();
            InheritanceList = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode*397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InterfaceModel);
        }

        public bool Equals(InterfaceModel other)
        {
            if (other == null) return false;
            if (this == other) return true;

            // List element equality is not checked in order to avoid infinite recursion of equality comparisons
            return base.Equals(other) &&
                   ListsHaveEqualSize(Methods, other.Methods) &&
                   ListsHaveEqualSize(Properties, other.Properties) &&
                   (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        protected static bool ListsHaveEqualSize<T>(IList<T> a, IList<T> b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            return a.Count == b.Count;
        }

        #endregion
    }
}
