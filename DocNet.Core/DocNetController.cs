
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
    /// <summary>
    /// This class is responsible for controlling the the entire documentation process.
    /// </summary>
    public class DocNetController : IDocNetController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DocNetController));

        private readonly ISolutionParser _solutionParser;
        private readonly IProjectParser _projectParser;
        private readonly ICsParser _csParser;
        private readonly IDocumentationGenerator _documentationGenerator;
        private readonly string _rootDirectoryName;

        /// <param name="config">The configuration that will be used when generating documentation.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="config"/> is null.</exception>
        public DocNetController(ControllerConfiguration config)
        {
            if(config == null)
                throw new ArgumentNullException("config");

            config.Validate();

            _solutionParser = config.SolutionParser;
            _projectParser = config.ProjectParser;
            _csParser = config.CsParser;
            _documentationGenerator = config.DocumentationGenerator;
            _rootDirectoryName = config.RootDirectoryName;
        }

        /// <summary>
        /// Generates documentation using the provided configuration.
        /// </summary>
        /// <returns>The program's exit status code.</returns>
        public DocNetStatus Execute(DocumentationSettings settings)
        {
            if(settings == null)
                throw new ArgumentNullException("settings");

            Log.Info(String.Empty);
            Log.Info("DocNet started...");

            try
            {
                settings.Validate();

                var outputDirectoryPath = Path.Combine(settings.OutputDirectoryPath, _rootDirectoryName);

                // Delete existing output directory
                var purgeResult = PurgeOutputDirectory(outputDirectoryPath);
                if(purgeResult != DocNetStatus.Success) return purgeResult;

                // Create output directory
                var createResult = CreateOutputDirectory(outputDirectoryPath);
                if(createResult != DocNetStatus.Success) return createResult;

                // Get list of unque input files
                var uniqueInputFileList = GetCsFileList(settings.InputFilePaths);
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
                    ParseCsFile(filePath, globalNamespace, settings.OutputMode);
                }

                // Generate documentation
                Log.Info("\nGenerating documentation...");
                _documentationGenerator.GenerateDocumentation(globalNamespace, outputDirectoryPath);
                
                Log.Info("Documentation generation complete.");
                Log.Info(String.Empty);
                Log.Info("Thank you for using DocNet!");             
                return DocNetStatus.Success;
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
        }

        #region Helper Methods

        private void ParseCsFile(string csFilePath, GlobalNamespaceModel globalNamespace, OutputMode mode)
        {
            using (var csFile = File.OpenRead(csFilePath))
            {
                _csParser.ParseIntoNamespace(csFile, globalNamespace, mode);
            }
        }

        private IList<string> GetCsFileList(IEnumerable<string> inputFilePaths)
        {
            var csFiles = new List<string>();
            foreach (var inputPath in inputFilePaths)
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
                            "Input path \"{0}\" is an invalid file type.", inputPath);
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

        private DocNetStatus PurgeOutputDirectory(string outputDirectoryPath)
        {
            try
            {
                if(Directory.Exists(outputDirectoryPath))
                {
                    Log.InfoFormat(CultureInfo.CurrentCulture,
                        "Purging output directory \"{0}\".", outputDirectoryPath);
                    Directory.Delete(outputDirectoryPath, true);
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
                    outputDirectoryPath
                    );
                Log.Debug(ex);
                return DocNetStatus.UnreachableOutputPath;
            }

            return DocNetStatus.Success;
        }

        private DocNetStatus CreateOutputDirectory(string outputDirectoryPath)
        {
            try
            {
                Log.InfoFormat(CultureInfo.CurrentCulture,
                    "Creating output directory \"{0}\".", outputDirectoryPath);
                Directory.CreateDirectory(outputDirectoryPath);
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
