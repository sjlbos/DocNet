
using System.Collections.Generic;

namespace DocNet.Core.Models.CSharp
{
    public interface ICsParentElement: IEnumerable<NestableCsElement>
    {
        void AddChild(NestableCsElement child);
        NestableCsElement this[string name] { get; }
        string FullName { get; }
    }
}
