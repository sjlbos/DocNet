using System.Collections.Generic;
using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class DelegateModel : CsTypeModel
    {
        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public string ReturnType { get; set; }
        public MethodDocComment DocComment { get; set; }
    }
}
