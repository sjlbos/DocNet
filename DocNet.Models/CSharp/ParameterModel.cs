using System;

namespace DocNet.Models.CSharp
{
    public enum ParameterKind
    {
        Value,
        Ref,
        Out,
        Params
    }

    public class ParameterModel : IEquatable<ParameterModel>
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public ParameterKind ParameterKind { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = ParameterKind.GetHashCode();
            hashCode = (hashCode * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0); 
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ParameterModel);
        }

        public bool Equals(ParameterModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return String.Equals(TypeName, other.TypeName) &&
                   String.Equals(Name, other.Name) &&
                   ParameterKind == other.ParameterKind;
        }

        #endregion
    }
}
