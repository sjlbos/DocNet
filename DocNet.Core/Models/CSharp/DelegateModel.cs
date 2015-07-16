using System.Collections.Generic;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class DelegateModel : CsTypeModel
    {
        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public string ReturnType { get; set; }
        public MethodDocComment DocComment { get; set; }

        public DelegateModel()
        {
            TypeParameters = new List<TypeParameterModel>();
            Parameters = new List<ParameterModel>();
        }
    }
}
