
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Markup
{
    public interface IMarkupGenerator
    {
        void GenerateMarkup(NamespaceModel globalNamespace, string outputDirectory);
    }
}
