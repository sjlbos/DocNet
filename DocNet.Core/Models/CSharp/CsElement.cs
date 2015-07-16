using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class CsElement : IEquatable<CsElement>
    {
        public string Name { get; set; }
        public abstract string FullName { get; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CsElement);
        }

        public bool Equals(CsElement other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return String.Equals(Name, other.Name) &&
                   String.Equals(FullName, other.FullName);
        }

        #endregion
    }
}
