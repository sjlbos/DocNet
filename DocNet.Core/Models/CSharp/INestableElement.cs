namespace DocNet.Core.Models.CSharp
{
    public interface INestableElement
    {
        IParentElement Parent { get; set; }
        string UniqueName { get; }
        string FullName { get; }

        bool IsDescendentOf(IParentElement parent);
        bool IsDirectDescendentOf(IParentElement parent);
    }
}
