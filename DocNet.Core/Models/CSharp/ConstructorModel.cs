using System;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class ConstructorModel : NestableCsElement, IEquatable<ConstructorModel>
    {
        public AccessModifier AccessModifier { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public bool IsStatic { get; set; }
        public MethodDocComment DocComment { get; set; }

        public ConstructorModel()
        {
            Parameters = new List<ParameterModel>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ AccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ IsStatic.GetHashCode();
            hashCode = (hashCode * 397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConstructorModel);
        }

        public bool Equals(ConstructorModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                   AccessModifier == other.AccessModifier &&
                   IsStatic == other.IsStatic &&
                   (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment)) &&
                   (Parameters == null ? (other.Parameters == null) : Parameters.SequenceEqual(other.Parameters));
        }

        #endregion
    }
}
