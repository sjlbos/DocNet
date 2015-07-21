using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public interface IPropertyInterface
{
    int MyNumberProperty { get; set; }
}

public abstract class ClassWithDuplicateSignatures : IEnumerable<string>, IPropertyInterface
{
    private readonly List<string> _enumberableCollection;

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<string> GetEnumerator()
    {
        return _enumberableCollection.GetEnumerator();
    }

    int IPropertyInterface.MyNumberProperty { get; set; }
    public float MyNumberProperty { get; set; }
}
