using System;
using System.IO;

namespace DocNet.Core.Output.Html.Views
{
    public abstract class BaseTemplate<T> : IDisposable
    {
        public TextWriter Writer { get; set; }
        public T Model { get; set; }
        public string RootDirectoryAbsolutePath { get; set; }
        public string ViewAbsolutePath { get; set; }

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
