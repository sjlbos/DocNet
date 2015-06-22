using System.Collections.Generic;

namespace DocNet.Models.CSharp
{
    public class ClassModel : InterfaceModel
    {
        public IList<ConstructorModel> Constructors;
        public IList<CsTypeModel> NestedTypes { get; set; } 

        public ClassModel()
        {
            Constructors = new List<ConstructorModel>();
            NestedTypes = new List<CsTypeModel>();
        }
    }
}
