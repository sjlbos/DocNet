
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Output.Html.Views
{
    public abstract class BodyTemplate : BaseTemplate
    {
        public virtual CsElement Model { get; set; }
    }

    public abstract class BodyTemplate<T> : BodyTemplate where T : CsElement
    {
        public new T Model
        {
            get { return base.Model as T; }
            set { base.Model = value; }
        }
    }
}
