
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

        public override string FullName
        {
            get
            {
                if (Name == null) return null;
                if (Parent != null)
                    return Parent.FullName + "." + Name;
                return Name;
            }
        }

        public override string UniqueName
        {
            get { return Name; }
        }

        #endregion
    }
}
