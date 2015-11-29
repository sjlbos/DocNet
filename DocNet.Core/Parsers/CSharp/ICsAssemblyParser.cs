using System.Reflection;
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Parsers.CSharp
{
    public interface ICsAssemblyParser
    {
        GlobalNamespaceModel GetGlobalNamespace(Assembly assembly, string docFileXml, OutputMode outputMode);
        void ParseIntoNamespace(Assembly assembly, string docFileXml, GlobalNamespaceModel globalNamespace, OutputMode outputMode);
    }
}
