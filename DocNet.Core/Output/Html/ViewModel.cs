
namespace DocNet.Core.Output.Html
{
    public class ViewModel<T>
    {
        public T Model { get; set; }
        public string RootDirectory { get; set; }
        public string ViewPath { get; set; }
    }
}
