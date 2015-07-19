using System;
using System.IO;
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Output.Html.Views
{
    public abstract class BaseTemplate : IDisposable
    {
        public virtual TextWriter Writer { get; set; }
        public string OutputDirectoryAbsolutePath { get; set; }
        public NamespaceModel GlobalNamespace { get; set; }

        public abstract void Execute();

        protected void WriteLiteral(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if(Writer == null)
                throw new InvalidOperationException("Writer property of template is null.");

            Writer.Write(text);
        }

        protected void Write(object value)
        {
            if (value == null)
                return;
            if(Writer == null)
                throw new InvalidOperationException("Writer property of template is null.");

            Writer.Write(value);
        }

        public override string ToString()
        {
            return Writer == null ? String.Empty : Writer.ToString();
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Writer != null)
                {
                    Writer.Dispose();
                }
            }
        }

        #endregion
    }
}
