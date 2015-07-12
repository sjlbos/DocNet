using System.Collections.Generic;
using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class ConstructorModel
    {
        public string Name { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public bool IsStatic { get; set; }
        public ClassAndStructModel Parent { get; set; }
        public MethodDocComment DocComment { get; set; }
    }
}
