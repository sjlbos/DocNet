using System;
using System.Collections.Generic;

namespace DocNet.Models.CSharp
{
    public class NamespaceModel : IEquatable<NamespaceModel>
    {
        public string Name { get; set; }
        public NamespaceModel ParentNamespace { get; set; }
        public IList<NamespaceModel> ChildNamespaces { get; set; } 
        public IList<ClassModel> Classes { get; set; }
        public IList<DelegateModel> Delegates { get; set; }
        public IList<EnumModel> Enums { get; set; }
        public IList<InterfaceModel> Interfaces { get; set; }
        public IList<StructModel> Structs { get; set; }

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

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode*397) ^ (ParentNamespace != null ? ParentNamespace.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (ChildNamespaces != null ? ChildNamespaces.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Classes != null ? Classes.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Delegates != null ? Delegates.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Enums != null ? Enums.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Interfaces != null ? Interfaces.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Structs != null ? Structs.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NamespaceModel);
        }

        public bool Equals(NamespaceModel other)
        {
            if (other == null) return false;
            if (this == other) return true;

            // List element equality is not checked in order to avoid infinite recursion of equality comparisons
            return String.Equals(Name, other.Name) &&
                   (ParentNamespace == null
                       ? (other.ParentNamespace == null)
                       : ParentNamespace.Equals(other.ParentNamespace)) &&
                   ListsHaveEqualSize(ChildNamespaces, other.ChildNamespaces) &&
                   ListsHaveEqualSize(Classes, other.Classes) &&
                   ListsHaveEqualSize(Delegates, other.Delegates) &&
                   ListsHaveEqualSize(Enums, other.Enums) &&
                   ListsHaveEqualSize(Structs, other.Structs);
        }

        private static bool ListsHaveEqualSize<T>(IList<T> a, IList<T> b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            return a.Count == b.Count;
        }

        #endregion
    }
}
