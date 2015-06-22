using System;

namespace DocNet.Models.CSharp
{
    public abstract class CsTypeModel
    {
        public string Name { get; set; }
        public NamespaceModel Namespace { get; set; }

        private CsTypeModel _parent;
        public CsTypeModel Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                Namespace = _parent.Namespace;
            }
        }

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
    }
}
 