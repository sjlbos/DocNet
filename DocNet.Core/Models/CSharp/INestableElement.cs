namespace DocNet.Core.Models.CSharp
{
    public interface INestableElement
    {
        IParentElement Parent { get; set; }
        string DisplayName { get; }
        string FullNameQualifier { get; }
        string FullDisplayName { get; }
        string InternalName { get; }
        string FullInternalName { get; }

        bool IsDescendentOf(IParentElement parent);
        bool IsDirectDescendentOf(IParentElement parent);
    }
}
