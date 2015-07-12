
using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class PropertyModel
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
        public AccessModifier AccessModifier { get; set; }
        public AccessModifier SetterAccessModifier { get; set; }
        public AccessModifier GetterAccessModifier { get; set; }
        public bool IsOverride { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsSealed { get; set; }
        public bool HidesBaseImplementation { get; set; }
        public InterfaceModel Parent { get; set; }
        public PropertyDocComment DocComment { get; set; }
    }
}
