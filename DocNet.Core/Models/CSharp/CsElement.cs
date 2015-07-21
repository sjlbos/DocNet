using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class CsElement : IEquatable<CsElement>
    {
        public virtual string Identifier { get; set; }
        public abstract string DisplayName { get; }
        public abstract string FullNameQualifier { get; }
        public abstract string FullDisplayName { get; }

        public abstract string InternalName { get; }
        public abstract string FullInternalName { get; }
        

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Identifier != null ? Identifier.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (InternalName != null ? InternalName.GetHashCode() : 0);
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
            return String.Equals(Identifier, other.Identifier) &&
                   String.Equals(InternalName, other.InternalName);
        }

        #endregion
    }
}
