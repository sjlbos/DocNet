
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

        private readonly ILog _log;
        private readonly ISolutionParser _solutionParser;
        private readonly IProjectParser _projectParser;
        private readonly ICsParser _csParser;
        private readonly IDocumentationGenerator _documentationGenerator;
        private readonly IEnumerable<string> _inputFilePaths; 
        private string _outputDirectoryPath;

        public DocNetController(ControllerConfiguration config)
        {
            if(config == null)
                throw new ArgumentNullException("config");

            config.Validate();

            _log = config.Logger;
            _solutionParser = config.SolutionParser;
            _projectParser = config.ProjectParser;
            _csParser = config.CsParser;
            _documentationGenerator = config.DocumentationGenerator;
            _outputDirectoryPath = config.OutputDirectoryPath;
            _inputFilePaths = config.InputFilePaths;
        }

        public DocNetStatus Execute()
        {
            _outputDirectoryPath = Path.Combine(_outputDirectoryPath, RootFolderName);

            var purgeResult = PurgeOutputDirectory();
            if(purgeResult != DocNetStatus.Success) return purgeResult;

            var createResult = CreateOutputDirectory();
            if (createResult != DocNetStatus.Success) return createResult;

            try
            {
                var globalNamespace = new NamespaceModel();
                foreach (var filePath in GetCsFileList())
                {
                    _log.InfoFormat("Parsing \"{0}\".", filePath);
                    ParseCsFile(filePath, globalNamespace);
                }
                _documentationGenerator.GenerateDocumentation(globalNamespace, _outputDirectoryPath);
            }
            catch (InvalidFileTypeException)
            {
                return DocNetStatus.InvalidInputPath;
            }

            return DocNetStatus.Success;
        }

        #region Helper Methods

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
                var directoryInfo = Directory.CreateDirectory(_outputDirectoryPath);
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

        #endregion
    }
}
