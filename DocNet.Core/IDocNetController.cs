using System.Collections.Generic;

namespace DocNet.Core

{
    public interface IDocNetController
    {
        DocNetStatus ExecuteSourceParser(IEnumerable<string> inputPaths, string outputPath, OutputMode mode = OutputMode.PublicOnly);
        DocNetStatus ExecuteAssemblyParser(IEnumerable<AssemblyXmlPair> inputPairs, string outputPath, OutputMode mode = OutputMode.PublicOnly);
    }
}
