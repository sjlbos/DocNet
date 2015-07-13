using System;
using System.IO;

namespace DocNet.Core.Markup.Views
{
    public abstract class BaseTemplate<T> : IDisposable
    {
        private readonly StringWriter _writer = new StringWriter();

        public T Model { get; set; }

        public abstract void Execute();

        protected void WriteLiteral(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            _writer.Write(text);
        }

        protected void Write(object value)
        {
            if (value == null)
                return;

            _writer.Write(value);
        }

        public override string ToString()
        {
            return _writer.ToString();
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
                if (_writer != null)
                {
                    _writer.Dispose();
                }
            }
        }

        #endregion
    }
}
