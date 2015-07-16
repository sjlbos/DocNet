using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class CsType: NamespaceElement, IEquatable<CsType>
    {
        public AccessModifier AccessModifier { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode*397) ^ AccessModifier.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CsType);
        }

        public bool Equals(CsType other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other) &&
                   AccessModifier == other.AccessModifier;
        }

        #endregion
    }
}
 