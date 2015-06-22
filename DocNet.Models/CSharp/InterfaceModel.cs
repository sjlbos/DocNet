using System.Collections.Generic;
using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class InterfaceModel : CsTypeModel
    {
        public IList<MethodModel> Methods;
        public IList<PropertyModel> Properties;
        public InterfaceDocComment DocComment { get; set; }

        public InterfaceModel()
        {
            Methods = new List<MethodModel>();
            Properties = new List<PropertyModel>();
        } 
    }
}
