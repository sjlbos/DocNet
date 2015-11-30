using System;
using DocNet.Core.Exceptions;
using DocNet.Core.Output;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;

namespace DocNet.Core
{
    /// <summary>
    /// An object containing the parameters necessary to instantiate an <see cref="IDocNetController"/> object.
    /// </summary>
    /// <remarks>
    /// ControllerConfiguration objects are used as a form of dependency injection, allowing external agents to 
    /// modify the behaviour of an <see cref="IDocNetController"/> instance by providing custom implementations of
    /// the components used by the controller.
    /// </remarks>
    public class ControllerConfiguration
    {
        /// <summary>
        /// The parser used to parse Visual Studio solution (.sln) files.
        /// </summary>
        public ISolutionParser SolutionParser { get; set; }

        /// <summary>
        /// The parser used to parse Visual Studio C# project (.csproj) files.
        /// </summary>
        public IProjectParser ProjectParser { get; set; }

        /// <summary>
        /// The parser used to parse C# (.cs) files.
        /// </summary>
        public ICsSourceParser CsSourceParser { get; set; }

        /// <summary>
        /// The documentation generator used to output documentation.
        /// </summary>
        public IDocumentationGenerator DocumentationGenerator { get; set; }

        /// <summary>
        /// The name of root directory of the output documentation tree.
        /// </summary>
        public string RootDirectoryName { get; set; }

        /// <summary>
        /// Checks that the ControllerConfiguration's properties are valid.
        /// </summary>
        /// <remarks>
        /// A configuration is invalid if any of the object's properties are null or empty.
        /// </remarks>
        /// <exception cref="ConfigurationException">Thrown when the ControllerConfiguration object contains an invalid configuration.</exception>
        public void Validate()
        {
            if(SolutionParser == null)
                throw new ConfigurationException("Solution parser is null.", DocNetStatus.InternalError);
            if(ProjectParser == null)
                throw new ConfigurationException("Project parser is null.", DocNetStatus.InternalError);
            if(CsSourceParser == null)
                throw new ConfigurationException("C# parser is null.", DocNetStatus.InternalError);
            if(DocumentationGenerator == null)
                throw new ConfigurationException("Documentation generator is null.", DocNetStatus.InternalError);
            if(String.IsNullOrWhiteSpace(RootDirectoryName))
                throw new ConfigurationException("Root directory name is not specified.", DocNetStatus.InvalidOutputPath);
        }
    }
}
