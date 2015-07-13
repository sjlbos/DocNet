using System;

namespace DocNet.Core.Models.CSharp
{
    public class TypeParameterModel : IEquatable<TypeParameterModel>
    {
        public string Name { get; set; }
        public string Constraint { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (Constraint != null ? Constraint.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeParameterModel);
        }

        public bool Equals(TypeParameterModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return String.Equals(Name, other.Name) &&
                   String.Equals(Constraint, other.Constraint);
        }

        #endregion
    }
}
