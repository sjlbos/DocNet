using System.Collections.Generic;

namespace DocNet.Models.CSharp
{
    public class StructModel : InterfaceModel
    {
        public IList<ConstructorModel> Constructors { get; set; }
        public IList<CsTypeModel> NestedTypes { get; set; } 

        public StructModel()
        {
            Constructors = new List<ConstructorModel>();
            NestedTypes = new List<CsTypeModel>();
        }
    }
}
