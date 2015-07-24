namespace DocNet.Core
{
    public interface IDocNetController
    {
        DocNetStatus Execute(DocumentationSettings settings);
    }
}
