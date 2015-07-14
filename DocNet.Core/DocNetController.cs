
using System;
using System.IO;
using System.Linq;
using DocNet.Core.Output;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;
using DocNet.Core.Models.CSharp;
using DocNet.Core.Models.VisualStudio;
using log4net;

namespace DocNet.Core
{
    public class DocNetController : IDocNetController
    {
        private readonly ILog _log;
        private readonly ISolutionParser _solutionParser;
        private readonly IProjectParser _projectParser;
        private readonly ICsParser _csParser;
        private readonly IDocumentationGenerator _documentationGenerator;

        private string _outputDirectoryPath;

        public DocNetController(ILog logger, ISolutionParser solutionParser, IProjectParser projectParser, ICsParser csParser, IDocumentationGenerator documentationGenerator)
        {
            if(logger == null)
                throw new ArgumentNullException("logger");
            if(solutionParser == null)
                throw new ArgumentNullException("solutionParser");
            if(projectParser == null)
                throw new ArgumentNullException("projectParser");
            if(csParser == null)
                throw new ArgumentNullException("csParser");
            if(documentationGenerator == null)
                throw new ArgumentNullException("documentationGenerator");

            _log = logger;
            _solutionParser = solutionParser;
            _projectParser = projectParser;
            _csParser = csParser;
            _documentationGenerator = documentationGenerator;
        }

        #region Public Interface

        public DocNetStatus DocumentSolution(string outputDirectoryPath, string solutionFilePath)
        {
            var outputStatus = ValidateAndCreateOutputDirectory(outputDirectoryPath);
            if (outputStatus != DocNetStatus.Success) return outputStatus;

            var inputStatus = ValidateInputFile(solutionFilePath);
            if (inputStatus != DocNetStatus.Success) return outputStatus;

            try
            {
                var globalNamespace = new NamespaceModel();
                ParseSolutionFile(solutionFilePath, globalNamespace);
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
            }
            catch (Exception ex)
            {
                // Todo: Add error handling for multiple exception types
            }
            return DocNetStatus.Success;
        }

        public DocNetStatus DocumentCsProject(string outputDirectoryPath, string projectFilePath)
        {
            var outputStatus = ValidateAndCreateOutputDirectory(outputDirectoryPath);
            if (outputStatus != DocNetStatus.Success) return outputStatus;

            var inputStatus = ValidateInputFile(projectFilePath);
            if (inputStatus != DocNetStatus.Success) return outputStatus;

            try
            {
                var globalNamespace = new NamespaceModel();
                ParseProjectFile(projectFilePath, globalNamespace);
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
            }
            catch (Exception ex)
            {
                // Todo: Add error handling for multiple exception types
            }
            return DocNetStatus.Success;
        }

        public DocNetStatus DocumentCsFiles(string outputDirectoryPath, params string[] csFilePaths)
        {
            var outputStatus = ValidateAndCreateOutputDirectory(outputDirectoryPath);
            if (outputStatus != DocNetStatus.Success) return outputStatus;

            try
            {
                var globalNamespace = new NamespaceModel();
                foreach (var csFilePath in csFilePaths)
                {
                    var inputStatus = ValidateInputFile(csFilePath);
                    if (inputStatus != DocNetStatus.Success) return inputStatus;
                    ParseCsFile(csFilePath, globalNamespace);
                }
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
            }
            catch (Exception ex)
            {
                // Todo: Add error handling for multiple exception types
            }
            return DocNetStatus.Success;
        }

        #endregion

        #region Input Validators

        private DocNetStatus ValidateAndCreateOutputDirectory(string outputDirectoryPath)
        {
            if (String.IsNullOrWhiteSpace(outputDirectoryPath))
            {
                try
                {
                    _outputDirectoryPath = Directory.GetCurrentDirectory();
                    return DocNetStatus.Success;
                }
                catch (UnauthorizedAccessException ex)
                {
                    _log.Error("Access denied. Unable to retrieve current directory.");
                    return DocNetStatus.InvalidOutputPath;
                }
            }

            try
            {
                var directoryInfo = Directory.CreateDirectory(outputDirectoryPath);
                _outputDirectoryPath = directoryInfo.FullName;
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

        private DocNetStatus ValidateInputFile(string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath))
            {
                _log.Error("Input file path is empty.");
                return DocNetStatus.InvalidInputPath;
            }

            if (!File.Exists(filePath))
            {
                _log.ErrorFormat("Unable to find input file \"{0}\".", filePath);
                return DocNetStatus.InvalidInputPath;
            }

            return DocNetStatus.Success;
        }

        #endregion

        #region Parser Wrappers

        private void ParseSolutionFile(string solutionFilePath, NamespaceModel globalNamespace)
        {
            _log.InfoFormat("Reading solution file \"{0}\".", solutionFilePath);
            var solutionModel = _solutionParser.ParseSolutionFile(solutionFilePath);
            ParseSolutionFile(solutionModel, globalNamespace);
        }

        private void ParseSolutionFile(SolutionModel solutionModel, NamespaceModel globalNamespace)
        {
            _log.InfoFormat("Proessing solution file \"{0}\"...", solutionModel.Name);
            if (!solutionModel.Projects.Any())
            {
                _log.InfoFormat("The solution \"{0}\" contains no C# project files.", solutionModel.Name);
                return;
            }

            foreach (var projectModel in solutionModel.Projects)
            {
                ParseProjectFile(projectModel, globalNamespace);
            }

            _log.Info("Solution processing complete.");
        }

        private void ParseProjectFile(string projectFilePath, NamespaceModel globalNamespace)
        {
            _log.InfoFormat("Reading project file \"{0}\".", projectFilePath);
            var projectModel = _projectParser.ParseProjectFile(projectFilePath);
            ParseProjectFile(projectModel, globalNamespace);
        }

        private void ParseProjectFile(ProjectModel projectModel, NamespaceModel globalNamespace)
        {
            _log.InfoFormat("Processing project file \"{0}\"...", projectModel.Name);
            if (!projectModel.IncludedFilePaths.Any())
            {
                _log.InfoFormat("The project \"{0}\" contains no C# files.", projectModel.Name);
                return;
            }

            foreach (var csFilePath in projectModel.IncludedFilePaths)
            {
                ParseCsFile(csFilePath, globalNamespace);
            }

            _log.Info("Project processing complete.");
        }

        private void ParseCsFile(string csFilePath, NamespaceModel globalNamespace)
        {
            _log.InfoFormat("Processing \"{0}\".", csFilePath);
            using (var csFile = File.OpenRead(csFilePath))
            {
                _csParser.ParseIntoNamespace(csFile, globalNamespace);   
            }
        }

        #endregion

        #region Processors



        #endregion
    }
}
