using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class CsTypeModel: IEquatable<CsTypeModel>
    {
        public string Name { get; set; }
        public NamespaceModel Namespace { get; set; }
        public CsTypeModel Parent { get; set; }
        public AccessModifier AccessModifier { get; set; }

        public string FullName
        {
            get
            {
                if(String.IsNullOrEmpty(Name)) return null;
                if(Parent != null && !String.IsNullOrEmpty(Parent.FullName)) return Parent.FullName + "." + Name;
                if(Namespace == null || String.IsNullOrEmpty(Namespace.Name)) return Name;
                return Namespace.Name + "." + Name;
            }
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode*397) ^ (Namespace != null ? Namespace.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Parent != null ? Parent.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ AccessModifier.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CsTypeModel);
        }

        public bool Equals(CsTypeModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name) &&
                   (Namespace == null ? (other.Name == null) : Namespace.Equals(other.Namespace)) &&
                   (Parent == null ? (other.Parent == null) : Parent.Equals(other.Parent)) &&
                   AccessModifier == other.AccessModifier;
        }

        #endregion
    }
}
 