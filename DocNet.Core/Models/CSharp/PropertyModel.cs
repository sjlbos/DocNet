
using System;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class PropertyModel : CsElement, INestableElement, IEquatable<PropertyModel>
    {
        public override string FullName
        {
            get
            {
                if (Name == null) return null;
                if (Parent != null)
                    return Parent.FullName + "_" + Name;
                return Name;
            }
        }

        #region INestableElement

        public IParentElement Parent { get; set; }

        public override string UniqueName
        {
            get { return Name; }
        }

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

        public string TypeName { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public AccessModifier SetterAccessModifier { get; set; }
        public AccessModifier GetterAccessModifier { get; set; }
        public bool IsOverride { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsSealed { get; set; }
        public bool HidesBaseImplementation { get; set; }
        public PropertyDocComment DocComment { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ HasGetter.GetHashCode();
            hashCode = (hashCode * 397) ^ HasSetter.GetHashCode();
            hashCode = (hashCode * 397) ^ AccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ SetterAccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ GetterAccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ IsOverride.GetHashCode();
            hashCode = (hashCode * 397) ^ IsStatic.GetHashCode();
            hashCode = (hashCode * 397) ^ IsAbstract.GetHashCode();
            hashCode = (hashCode * 397) ^ IsVirtual.GetHashCode();
            hashCode = (hashCode * 397) ^ IsSealed.GetHashCode();
            hashCode = (hashCode * 397) ^ HidesBaseImplementation.GetHashCode();
            hashCode = (hashCode * 397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PropertyModel);
        }

        public bool Equals(PropertyModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                   String.Equals(TypeName, other.TypeName) &&
                   HasGetter == other.HasGetter &&
                   HasSetter == other.HasSetter &&
                   AccessModifier == other.AccessModifier &&
                   SetterAccessModifier == other.SetterAccessModifier &&
                   GetterAccessModifier == other.GetterAccessModifier &&
                   IsOverride == other.IsOverride &&
                   IsStatic == other.IsStatic &&
                   IsAbstract == other.IsAbstract &&
                   IsVirtual == other.IsVirtual &&
                   IsSealed == other.IsSealed &&
                   HidesBaseImplementation == other.HidesBaseImplementation &&
                   (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        #endregion
    }
}
