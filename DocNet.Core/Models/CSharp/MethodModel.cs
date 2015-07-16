using System.Collections.Generic;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class MethodModel
    {
        public string Name { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public string ReturnType { get; set; }
        public bool HidesBaseImplementation { get; set; }
        public bool IsOverride { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsAsync { get; set; }
        public bool IsSealed { get; set; }
        public InterfaceModel Parent { get; set; }
        public MethodDocComment DocComment { get; set; }

        public MethodModel()
        {
            TypeParameters = new List<TypeParameterModel>();
            Parameters = new List<ParameterModel>();
        }
    }
}
