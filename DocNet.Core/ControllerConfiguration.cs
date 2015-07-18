using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocNet.Core.Exceptions;
using DocNet.Core.Output;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;
using log4net;

namespace DocNet.Core
{
    public class ControllerConfiguration
    {
        public ILog Logger { get; set; }
        public ISolutionParser SolutionParser { get; set; }
        public IProjectParser ProjectParser { get; set; }
        public ICsParser CsParser { get; set; }
        public IDocumentationGenerator DocumentationGenerator { get; set; }
        public string OutputDirectoryPath { get; set; }
        public IEnumerable<string> InputFilePaths { get; set; }

        public void Validate()
        {
            if(Logger == null)
                throw new ConfigurationException("Logger is null.", DocNetStatus.InternalError);
            if(SolutionParser == null)
                throw new ConfigurationException("Solution parser is null.", DocNetStatus.InternalError);
            if(ProjectParser == null)
                throw new ConfigurationException("Project parser is null.", DocNetStatus.InternalError);
            if(CsParser == null)
                throw new ConfigurationException("C# parser is null.", DocNetStatus.InternalError);
            if(DocumentationGenerator == null)
                throw new ConfigurationException("Documentation generator is null.", DocNetStatus.InternalError);

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
        }

        private void ValidateInputFilePaths()
        {
            if(InputFilePaths == null || !InputFilePaths.Any())
                throw new ConfigurationException("No input files specified.", DocNetStatus.InvalidProgramArguments);
            
            if(InputFileListContainsDuplicatePaths())
                throw new ConfigurationException("Input file list contains duplicate paths.", DocNetStatus.InvalidProgramArguments);

            foreach (var inputFilePath in InputFilePaths)
            {
                ValidateInputFilePath(inputFilePath);
            }
        }

        private void ValidateInputFilePath(string filePath)
        {
            if(String.IsNullOrWhiteSpace(filePath))
                throw new ConfigurationException("Input file path is empty.", DocNetStatus.InvalidInputPath);

            if(!Path.IsPathRooted(filePath))
                throw new ConfigurationException(String.Format(CultureInfo.CurrentCulture,
                    "The input file path \"{0}\" is not an absolute path.", filePath),
                    DocNetStatus.InvalidInputPath
                    );

            if(!File.Exists(filePath))
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
