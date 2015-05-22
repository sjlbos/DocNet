using System.Collections.Generic;

namespace DocNet.Models.CSharp
{
    public class NamespaceModel
    {
        public IList<ClassModel> Classes { get; set; }
        public IList<InterfaceModel> Interfaces { get; set; }
        public IList<MethodModel> Delegates { get; set; }
        public IList<StructModel> Structs { get; set; }
        public IList<EnumModel> Enums { get; set; }

        public NamespaceModel()
        {
            Classes = new List<ClassModel>();
            Interfaces = new List<InterfaceModel>();
            Delegates = new List<MethodModel>();
            Structs = new List<StructModel>();
            Enums = new List<EnumModel>();
        }
    }
}
