
namespace DocNet.Core.Models.CSharp
{
    public class NamespaceModel : NamespaceBase, INestableElement
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

        #region Properties

        public override string DisplayName
        {
            get { return Identifier; }
        }

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
                if (InternalName == null) return null;
                if (Parent.FullInternalName == null) return InternalName;
                return Parent.FullInternalName + "." + InternalName;
            }
        }

        public override string InternalName
        {
            get { return Identifier; }
        }

        #endregion
    }
}
