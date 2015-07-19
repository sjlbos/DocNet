
namespace DocNet.Core.Output.Html.Views
{
    public abstract class BodyTemplate : BaseTemplate { }

    public abstract class BodyTemplate<T> : BodyTemplate
    {
        public T Model { get; set; }
    }
}
