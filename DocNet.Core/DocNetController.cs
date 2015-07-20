
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
        private static readonly ILog Log = LogManager.GetLogger(typeof(DocNetController));

        private readonly ControllerConfiguration _config;
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
        }

        public DocNetStatus Execute()
        {
            Log.Info(String.Empty);
            Log.Info("DocNet started...");

            try
            {
                // Read in configuration
                UnpackAndValidateConfig();

                _outputDirectoryPath = Path.Combine(_outputDirectoryPath, _config.RootDirectoryName);

                // Delete existing output directory
                var purgeResult = PurgeOutputDirectory();
                if(purgeResult != DocNetStatus.Success) return purgeResult;

                // Create output directory
                var createResult = CreateOutputDirectory();
                if(createResult != DocNetStatus.Success) return createResult;

                // Get list of unque input files
                var uniqueInputFileList = GetCsFileList();
                Log.Info(String.Empty);
                Log.Info("Documentation will be generated using the following input files:");
                foreach(var filePath in uniqueInputFileList)
                {
                    Log.Info("    " + filePath);
                }

                // Parse each input file
                var globalNamespace = new GlobalNamespaceModel();
                Log.Info(String.Empty);
                foreach(var filePath in uniqueInputFileList)
                {
                    Log.InfoFormat("Parsing \"{0}\".", filePath);
                    ParseCsFile(filePath, globalNamespace);
                }

                // Generate documentation
                Log.Info("\nGenerating documentation...");
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
                Log.Info("Documentation generation complete.");
                Log.Info(String.Empty);
                Log.Info("Thank you for using DocNet!");
            }
            catch(ConfigurationException ex)
            {
                Log.Debug(ex);
                Log.Fatal(ex.Message);
                return ex.Status;
            }
            catch(InvalidFileTypeException ex)
            {
                Log.Debug(ex);
                return DocNetStatus.InvalidInputPath;
            }
            catch(CsParsingException ex)
            {
                Log.Debug(ex);
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

        private void ParseCsFile(string csFilePath, GlobalNamespaceModel globalNamespace)
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
                        Log.ErrorFormat(CultureInfo.CurrentCulture,
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
                    Log.WarnFormat(CultureInfo.CurrentCulture,
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
                    Log.InfoFormat(CultureInfo.CurrentCulture,
                        "Purging output directory \"{0}\".", _outputDirectoryPath);
                    Directory.Delete(_outputDirectoryPath, true);
                }
            }
            catch(UnauthorizedAccessException ex)
            {
                Log.Error("Access denied on output directory.");
                Log.Debug(ex);
                return DocNetStatus.UnreachableInputPath;
            }
            catch(ArgumentException ex)
            {
                Log.Error("Output path contains invalid characters.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch(PathTooLongException ex)
            {
                Log.Error("Output path is too long.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch(IOException ex)
            {
                Log.ErrorFormat(CultureInfo.CurrentCulture,
                    "Unable to delete output directory \"{0}\". The folder may be in use by another process.",
                    _outputDirectoryPath
                    );
                Log.Debug(ex);
                return DocNetStatus.UnreachableOutputPath;
            }

            return DocNetStatus.Success;
        }

        private DocNetStatus CreateOutputDirectory()
        {
            try
            {
                Log.InfoFormat(CultureInfo.CurrentCulture,
                    "Creating output directory \"{0}\".", _outputDirectoryPath);
                Directory.CreateDirectory(_outputDirectoryPath);
                return DocNetStatus.Success;
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error("Access denied on output directory.");
                Log.Debug(ex);
                return DocNetStatus.UnreachableOutputPath;
            }
            catch (ArgumentException ex)
            {
                Log.Error("Output path contains invalid characters.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (NotSupportedException ex)
            {
                Log.Error("Output path contains invalid characters.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (PathTooLongException ex)
            {
                Log.Error("Output path is too long.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (DirectoryNotFoundException ex)
            {
                Log.Error("Part of the output path could not be found.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
            catch (IOException ex)
            {
                Log.Error("Output path points to a file or unrecognized network.");
                Log.Debug(ex);
                return DocNetStatus.InvalidOutputPath;
            }
        }

        #endregion
    }
}
