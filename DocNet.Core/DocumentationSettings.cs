using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocNet.Core.Exceptions;

namespace DocNet.Core
{
    /// <summary>
    /// An object containing the parameters required by DocNet to carry out a documentation generation session.
    /// </summary>
    public class DocumentationSettings
    {
        /// <summary>
        /// Path to the directory where DocNet will create a directory to hold the output documentation files.
        /// </summary>
        public string OutputDirectoryPath { get; set; }

        /// <summary>
        /// List of .cs, .csproj, and/or .sln files that will be parsed by DocNet to produce documentation.
        /// </summary>
        public IEnumerable<string> InputFilePaths { get; set; }

        /// <summary>
        /// The output mode of the generated documentation. Controls which C# elements will be documented.
        /// </summary>
        public OutputMode OutputMode { get; set; }

        /// <summary>
        /// Checks that the DocumentationSetting's configuration is valid.
        /// </summary>
        /// <remarks>
        /// <para>A configuration may invalid  under the following circumstances:</para>
        /// <list type="bullet">
        ///     <item><term>The output directory is null or contains only whitespace.</term></item>
        ///     <item><term>The output directory is not an absolute path.</term></item>
        ///     <item><term>The output directory does not exist.</term></item>
        ///     <item><term>No input paths are specified or the input file list is null.</term></item>
        ///     <item><term>The list of input files contains duplicate paths.</term></item>
        ///     <item><term>An input file path is null or contains only whitespace.</term></item>
        ///     <item><term>An input file path is not an absolute path.</term></item> 
        ///     <item><term>An input file path is invalid or points to a directory.</term></item> 
        /// </list>
        /// </remarks>
        /// <exception cref="ConfigurationException">Thrown when the DocumentationSettings object contains an invalid configuration.</exception>
        public void Validate()
        {
            ValidateOutputDirectory();
            ValidateInputFilePaths();
        }

        private void ValidateOutputDirectory()
        {
            if (String.IsNullOrWhiteSpace(OutputDirectoryPath))
            {
                throw new ConfigurationException("Output directory path not specified.", DocNetStatus.InvalidProgramArguments);
            }

            if (!Path.IsPathRooted(OutputDirectoryPath))
            {
                throw new ConfigurationException(String.Format(CultureInfo.CurrentCulture,
                    "Output directory path \"{0}\" is not an absolute path.", OutputDirectoryPath),
                    DocNetStatus.InvalidOutputPath
                    );
            }

            if (!Directory.Exists(OutputDirectoryPath))
            {
                throw new ConfigurationException(String.Format(CultureInfo.CurrentCulture,
                    "Output directory \"{0}\" does not exist.", OutputDirectoryPath),
                    DocNetStatus.InvalidOutputPath);
            }
        }

        private void ValidateInputFilePaths()
        {
            if (InputFilePaths == null || !InputFilePaths.Any())
                throw new ConfigurationException("No input files specified.", DocNetStatus.InvalidProgramArguments);

            if (InputFileListContainsDuplicatePaths())
                throw new ConfigurationException("Input file list contains duplicate paths.", DocNetStatus.InvalidProgramArguments);

            foreach (var inputFilePath in InputFilePaths)
            {
                ValidateInputFilePath(inputFilePath);
            }
        }

        private void ValidateInputFilePath(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
                throw new ConfigurationException("Input file path is empty.", DocNetStatus.InvalidInputPath);

            if (!Path.IsPathRooted(filePath))
                throw new ConfigurationException(String.Format(CultureInfo.CurrentCulture,
                    "The input file path \"{0}\" is not an absolute path.", filePath),
                    DocNetStatus.InvalidInputPath
                    );

            if (!File.Exists(filePath))
                throw new ConfigurationException(String.Format(CultureInfo.CurrentCulture,
                    "The input file path \"{0}\" could not be found.", filePath),
                    DocNetStatus.InvalidInputPath);
        }

        private bool InputFileListContainsDuplicatePaths()
        {
            return InputFilePaths.Distinct().Count() != InputFilePaths.Count();
        }
    }
}
