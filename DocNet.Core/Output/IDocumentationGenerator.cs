
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Output
{
    public interface IDocumentationGenerator
    {
        void GenerateDocumentation(GlobalNamespaceModel globalNamespace, string outputDirectory);
    }
}
