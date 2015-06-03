using System.Collections.Generic;
using DocNet.Models.CSharp;

namespace DocNet.Core.Parsers.CSharp
{
    public interface ICsParser
    {
        IEnumerable<NamespaceModel> GetNamespaceTrees();
    }
}
