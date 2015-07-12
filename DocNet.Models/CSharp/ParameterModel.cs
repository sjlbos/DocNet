namespace DocNet.Models.CSharp
{
    public enum ParameterKind
    {
        Value,
        Ref,
        Out,
        Params
    }

    public class ParameterModel
    {
        public string TypeName { get; set; }
        public string Name { get; set; }
        public ParameterKind ParameterKind { get; set; }
    }
}
