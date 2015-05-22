using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public enum ParameterType
    {
        Value,
        Ref,
        Out
    }

    public class ParameterModel
    {
        public PropertyDocComment DocComment { get; set; }
        public string FullTypeName { get; set; }
        public string TypeName { get; set; }
        public string ParameterName { get; set; }
        public ParameterType ParameterType { get; set; }
    }
}
