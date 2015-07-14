
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Output
{
    public interface IDocumentationGenerator
    {
        void GenerateDocumentation(NamespaceModel globalNamespace, string outputDirectory);
    }
}
