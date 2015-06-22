using System;
using System.Collections.Generic;

namespace DocNet.Models.CSharp
{
    public class NamespaceModel
    {
        public string Name { get; set; }
        public NamespaceModel ParentNamespace { get; set; }
        public IList<NamespaceModel> ChildNamespaces { get; set; } 
        public IList<ClassModel> Classes { get; set; }
        public IList<InterfaceModel> Interfaces { get; set; }
        public IList<DelegateModel> Delegates { get; set; }
        public IList<StructModel> Structs { get; set; }
        public IList<EnumModel> Enums { get; set; }

        public string FullName
        {
            get
            {
                if (String.IsNullOrEmpty(Name)) return null;
                if (ParentNamespace == null || String.IsNullOrEmpty(ParentNamespace.Name)) return Name;
                return ParentNamespace.Name + "." + Name;
            }
        }

        public NamespaceModel()
        {
            ChildNamespaces = new List<NamespaceModel>();
            Classes = new List<ClassModel>();
            Interfaces = new List<InterfaceModel>();
            Delegates = new List<DelegateModel>();
            Structs = new List<StructModel>();
            Enums = new List<EnumModel>();
        }
    }
}
