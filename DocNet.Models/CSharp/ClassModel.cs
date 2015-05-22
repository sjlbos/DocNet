using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class ClassModel
    {
        public ClassDocComment DocComment { get; set; }
        public string Namespace { get; set; }
        public string FullyQualifiedName { get; set; }
        public string ClassName { get; set; }
    }
}
