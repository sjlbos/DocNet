
using System.Collections.Generic;
using System.IO;

namespace DocNet.Core.Output.Html.Views
{
    public abstract class PageTemplate : BaseTemplate
    {
        public IEnumerable<string> CssFiles { get; set; }
        public IEnumerable<string> ScriptFiles { get; set; } 

        private BodyTemplate _body;
        public BodyTemplate Body
        {
            get { return _body; }
            set
            {
                _body = value;
                _body.Writer = Writer;
            }
        }

        private TextWriter _writer;
        public override TextWriter Writer
        {
            get { return _writer; }
            set
            {
                if (Body != null)
                {
                    Body.Writer = value; 
                }
                _writer = value;
            }
        }
    }
}
