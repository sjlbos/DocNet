using System.IO;
using DocNet.Models.CSharp;

namespace DocNet.Core.Parsers.CSharp
{
    public interface ICsParser
    {
        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        NamespaceModel GetGlobalNamespace(string sourceCode);

        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <returns>A model of the input source code's global namespace.</returns>s
        NamespaceModel GetGlobalNamespace(Stream sourceFileStream);
    }
}
