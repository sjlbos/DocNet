
using System.Collections.Generic;

namespace DocNet.Core.Models.CSharp
{
    public interface ICsParentElement: IEnumerable<NestableCsElement>
    {
        void AddChild(NestableCsElement child);
        NestableCsElement this[string uniqueName] { get; }
        string FullName { get; }
    }
}
