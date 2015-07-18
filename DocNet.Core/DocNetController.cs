
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocNet.Core.Exceptions;
using DocNet.Core.Output;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;
using DocNet.Core.Models.CSharp;
using log4net;

namespace DocNet.Core
{
    public class DocNetController : IDocNetController
    {
        private const string RootFolderName = "html";

        private readonly ControllerConfiguration _config;

        private readonly ILog _log;
        private ISolutionParser _solutionParser;
        private IProjectParser _projectParser;
        private ICsParser _csParser;
        private IDocumentationGenerator _documentationGenerator;
        private IEnumerable<string> _inputFilePaths; 
        private string _outputDirectoryPath;

        public DocNetController(ControllerConfiguration config)
        {
            if(config == null)
                throw new ArgumentNullException("config");

            _config = config;
            _log = LogManager.GetLogger(typeof(DocNetController));
        }

        public DocNetStatus Execute()
        {
            _log.Info(String.Empty);
            _log.Info("DocNet started...");

            try
            {
                // Read in configuration
                UnpackAndValidateConfig();

                _outputDirectoryPath = Path.Combine(_outputDirectoryPath, RootFolderName);

                // Delete existing output directory
                var purgeResult = PurgeOutputDirectory();
                if(purgeResult != DocNetStatus.Success) return purgeResult;

                // Create output directory
                var createResult = CreateOutputDirectory();
                if(createResult != DocNetStatus.Success) return createResult;

                // Get list of unque input files
                var uniqueInputFileList = GetCsFileList();
                _log.Info(String.Empty);
                _log.Info("Documentation will be generated using the following input files:");
                foreach(var filePath in uniqueInputFileList)
                {
                    _log.Info("    " + filePath);
                }

                // Parse each input file
                var globalNamespace = new NamespaceModel();
                _log.Info(String.Empty);
                foreach(var filePath in uniqueInputFileList)
                {
                    _log.InfoFormat("Parsing \"{0}\".", filePath);
                    ParseCsFile(filePath, globalNamespace);
                }

                // Generate documentation
                _log.Info("\nGenerating documentation...");
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
                _log.Info("Documentation generation complete.");
                _log.Info(String.Empty);
                _log.Info("Thank you for using DocNet!");
            }
            catch(ConfigurationException ex)
            {
                _log.Debug(ex);
                _log.Fatal(ex.Message);
                return ex.Status;
            }
            catch(InvalidFileTypeException)
            {
                return DocNetStatus.InvalidInputPath;
            }
            catch(CsParsingException)
            {
                return DocNetStatus.ParsingError;
            }

            return DocNetStatus.Success;
        }

        #region Helper Methods

        private void UnpackAndValidateConfig()
        {
            _config.Validate();

            _solutionParser = _config.SolutionParser;
            _projectParser = _config.ProjectParser;
            _csParser = _config.CsParser;
            _documentationGenerator = _config.DocumentationGenerator;
            _outputDirectoryPath = _config.OutputDirectoryPath;
            _inputFilePaths = _config.InputFilePaths;
        }

        private void ParseCsFile(string csFilePath, NamespaceModel globalNamespace)
        {
            using (var csFile = File.OpenRead(csFilePath))
            {
                _csParser.ParseIntoNamespace(csFile, globalNamespace);
            }
        }

        private IList<string> GetCsFileList()
        {
            var csFiles = new List<string>();
            foreach (var inputPath in _inputFilePaths)
            {
                string fileExtension = Path.GetExtension(inputPath);
                switch (fileExtension)
                {
                    case ".cs":
                        csFiles.Add(inputPath);
                        break;
                    case ".csproj":
                        var projectFiles = _projectParser.ParseProjectFile(inputPath).IncludedFilePaths;
                        csFiles.AddRange(projectFiles);
                        break;                 
                    case ".sln":
                        var solution = _solutionParser.ParseSolutionFile(inputPath);
                        foreach (var project in solution.Projects)
                        {
                            csFiles.AddRange(project.IncludedFilePaths);
                        }
                        break;
                    default:
                        _log.ErrorFormat(CultureInfo.CurrentCulture,
                            "Input path \"{0}\" is a valid file type.", inputPath);
                        throw new InvalidFileTypeException(inputPath);
                }
            }
            return RemoveDuplicatePaths(csFiles);
        }

        private IList<string> RemoveDuplicatePaths(IList<string> inputPaths)
        {
            var uniquePaths = inputPaths.Distinct().ToList();
            if (uniquePaths.Count() != inputPaths.Count)
            {
                var duplicates = inputPaths.Except(uniquePaths);
                foreach (var duplicate in duplicates)
                {
                    _log.WarnFormat(CultureInfo.CurrentCulture,
                        "Duplicate input file \"{0}\" will be skipped.", duplicate);
                }
            }
            return uniquePaths;
        }

        private DocNetStatus PurgeOutputDirectory()
        {
            try
            {
                if(Directory.Exists(_outputDirectoryPath))
                {
                    _log.InfoFormat(CultureInfo.CurrentCulture,
                        "Purging output directory \"{0}\".", _outputDirectoryPath);
                    Directory.Delete(_outputDirectoryPath, true);
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                _log.Error("Access denied on output directory.");
                _log.Debug(ex);
                return DocNetStatus.UnreachableInputPath;
            }
            catch(ArgumentException ex)
            {
                _log.Error("Output path contains invalid characters.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch(PathTooLongException ex)
            {
                _log.Error("Output path is too long.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch(IOException ex)
            {
                _log.ErrorFormat(CultureInfo.CurrentCulture,
                    "Unable to delete output directory \"{0}\". The folder may be in use by another process.",
                    _outputDirectoryPath
                    );
                _log.Debug(ex);
                return DocNetStatus.UnreachableOutputPath;
            }

            return DocNetStatus.Success;
        }

        private DocNetStatus CreateOutputDirectory()
        {
            try
            {
                _log.InfoFormat(CultureInfo.CurrentCulture,
                    "Creating output directory \"{0}\".", _outputDirectoryPath);
                Directory.CreateDirectory(_outputDirectoryPath);
                return DocNetStatus.Success;
            }
            catch (UnauthorizedAccessException ex)
            {
                _log.Error("Access denied on output directory.");
                _log.Debug(ex);
                return DocNetStatus.UnreachableOutputPath;
            }
            catch (ArgumentException ex)
            {
                _log.Error("Output path contains invalid characters.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (NotSupportedException ex)
            {
                _log.Error("Output path contains invalid characters.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (PathTooLongException ex)
            {
                _log.Error("Output path is too long.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (DirectoryNotFoundException ex)
            {
                _log.Error("Part of the output path could not be found.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (IOException ex)
            {
                _log.Error("Output path points to a file or unrecognized network.");
                _log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
        }

        #endregion
    }
}
