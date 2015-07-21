using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class CsType: CsElement, INestableElement, IEquatable<CsType>
    {
        #region INestableElement Interface

        public IParentElement Parent { get; set; }

        public bool IsDirectDescendentOf(IParentElement parent)
        {
            return parent != null && parent == Parent;
        }

        public bool IsDescendentOf(IParentElement parent)
        {
            if (parent == null) return false;
            if (parent == this.Parent) return true;
            var localParentAsChild = this.Parent as INestableElement;
            if (localParentAsChild == null) return false;
            return localParentAsChild.IsDescendentOf(parent);
        }

        #endregion

        public override string FullNameQualifier
        {
            get { return Parent != null ? Parent.FullDisplayName : null; }
        }

        public override string FullDisplayName
        {
            get
            {
                if (DisplayName == null) return null;
                if (Parent == null || Parent.FullDisplayName == null) return DisplayName;
                return FullNameQualifier + "." + DisplayName;
            }
        }

        public override string FullInternalName
        {
            get
            {
                if (Identifier == null) return null;
                if (Parent != null)
                    return Parent.FullInternalName + "_" + InternalName;
                return Identifier;
            }
        }

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
 