using System.IO;
using DocNet.Core.Models.CSharp;

namespace DocNet.Core.Parsers.CSharp
{
    public interface ICsParser
    {
        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        /// <returns>A model of the input source code's global namespace.</returns>
        GlobalNamespaceModel GetGlobalNamespace(string sourceCode, OutputMode outputMode);

        /// <summary>
        /// Parses C# source code and adds the parsed elements to the specified namespace.
        /// </summary>
        /// <param name="sourceCode">A string containing C# source code.</param>
        /// <param name="globalNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        void ParseIntoNamespace(string sourceCode, GlobalNamespaceModel globalNamespace, OutputMode outputMode);

        /// <summary>
        /// Parses C# source code and returns a model of the source code's global namespace, including all child namespaces,
        /// types, and documentation comments.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        /// <returns>A model of the input source code's global namespace.</returns>s
        GlobalNamespaceModel GetGlobalNamespace(Stream sourceFileStream, OutputMode outputMode);

        /// <summary>
        /// Parses C# source code and adds the parsed elments to the specified namespace.
        /// </summary>
        /// <param name="sourceFileStream">A Stream object pointing to a C# source code file.</param>
        /// <param name="globalNamespace">A NamespaceModel object to which the types and namespaces contained in the input source code will be added.</param>
        /// <param name="outputMode">The program output mode which controls which elements will be documented.</param>
        void ParseIntoNamespace(FileStream sourceFileStream, GlobalNamespaceModel globalNamespace, OutputMode outputMode);
    }
}
