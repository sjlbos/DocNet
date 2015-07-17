using System;

namespace Foo
{
    public partial interface IBar
    {
        void Do();
    }

    public partial class Bar : IBar
    {
        public string PropertyA { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void DoNot()
        {
            throw new NotImplementedException();
        }

        public void Do()
        {
            throw new NotImplementedException();
        }
    }

    public partial struct Baz : IBar
    {
        public string PropertyA { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void DoNot()
        {
            throw new NotImplementedException();
        }

        public void Do()
        {
            throw new NotImplementedException();
        }
    }
}

namespace Foo
{
    public partial interface IBar: IDisposable
    {
        void DoNot();
    }

    public partial class Bar : IEquatable<Bar>
    {
        public string PropertyB { get; set; }

        public bool Equals(Bar other)
        {
            throw new NotImplementedException();
        }
    }

    public partial struct Baz : IEquatable<Baz>
    {
        public string PropertyB { get; set; }

        public bool Equals(Baz other)
        {
            throw new NotImplementedException();
        }
    }

}
