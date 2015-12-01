
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly ICsSourceParser _csSourceParser;
        private readonly ICsAssemblyParser _csAssemblyParser;
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
            _csSourceParser = config.CsSourceParser;
            _csAssemblyParser = config.CsAssemblyParser;
            _documentationGenerator = config.DocumentationGenerator;
            _rootDirectoryName = config.RootDirectoryName;
        }

        /// <summary>
        /// Generates documentation using the provided source files. Defers to ExecuteSourceParser(inputPaths, outputPath, OutputMode.publicOnly)
        /// </summary>
        /// <param name="inputPaths">The list of source paths to use as input</param>
        /// <param name="outputPath">The path to the output directory, where the results should be written</param>
        /// <returns>The program's exit status code.</returns>

        public DocNetStatus ExecuteSourceParser(IEnumerable<string> inputPaths, string outputPath)
        {
            return ExecuteSourceParser(inputPaths, outputPath, OutputMode.PublicOnly);
        }

        /// <summary>
        /// Generates documentation using the provided source files.
        /// </summary>
        /// <param name="inputPaths">The list of source paths to use as input</param>
        /// <param name="outputPath">The path to the output directory, where the results should be written</param>
        /// <param name="mode">The output mode to use when parsing the files</param>
        /// <returns>The program's exit status code.</returns>
        public DocNetStatus ExecuteSourceParser(IEnumerable<string> inputPaths, string outputPath, OutputMode mode)
        {
            Func<IEnumerable<string>, OutputMode, GlobalNamespaceModel> generateGlobalNamespaceModelMethod = GenerateGlobalNamespaceModelFromSource;
            return Execute(generateGlobalNamespaceModelMethod, inputPaths, outputPath, mode);
        }

        /// <summary>
        /// Generates documentation using the provided assembly files. Defers to ExecuteAssemblyParser(inputPairs, outputPath, OutputMode.publicOnly)
        /// </summary>
        /// <param name="inputPairs">The list of AssemblyXmlPair objects to use as input</param>
        /// <param name="outputPath">The path to the output directory, where the results should be written</param>
        /// <returns>The program's exit status code.</returns>
        public DocNetStatus ExecuteAssemblyParser(IEnumerable<AssemblyXmlPair> inputPairs, string outputPath)
        {
            return ExecuteAssemblyParser(inputPairs, outputPath, OutputMode.PublicOnly);
        }

        /// <summary>
        /// Generates documentation using the provided assembly files.
        /// </summary>
        /// <param name="inputPairs">The list of AssemblyXmlPair objects to use as input</param>
        /// <param name="outputPath">The path to the output directory, where the results should be written</param>
        /// <param name="mode">The output mode to use when parsing the files</param>
        /// <returns>The program's exit status code.</returns>
        public DocNetStatus ExecuteAssemblyParser(IEnumerable<AssemblyXmlPair> inputPairs, string outputPath, OutputMode mode = OutputMode.PublicOnly)
        {
            Func<IEnumerable<AssemblyXmlPair>, OutputMode, GlobalNamespaceModel> generateGlobalNamespaceModelMethod = GenerateGlobalNamespaceModelFromAssembly;
            return Execute(generateGlobalNamespaceModelMethod, inputPairs, outputPath, mode);
        }

        /// <summary>
        /// Generates documentation using the provided files.
        /// </summary>
        /// <param name="generateGlobalNamespaceModel">The method to use to generate the global namespace model</param>
        /// <param name="inputPairs">The list of files to use as input</param>
        /// <param name="outputPath">The path to the output directory, where the results should be written</param>
        /// <param name="mode">The output mode to use when parsing the files</param>
        /// <returns>The program's exit status code.</returns>
        private DocNetStatus Execute<T>(Func<IEnumerable<T>, OutputMode, GlobalNamespaceModel> generateGlobalNamespaceModel, IEnumerable<T> inputPaths, string outputPath, OutputMode mode)
        {
            if(inputPaths == null)
                throw new ArgumentNullException("inputPaths");
            if (outputPath == null)
                throw new ArgumentNullException("outputPath");

            Log.Info(String.Empty);
            Log.Info("DocNet started...");

            try
            {
                var outputDirectoryPath = Path.Combine(outputPath, _rootDirectoryName);

                // Delete existing output directory
                var purgeResult = PurgeOutputDirectory(outputDirectoryPath);
                if(purgeResult != DocNetStatus.Success) return purgeResult;

                // Create output directory
                var createResult = CreateOutputDirectory(outputDirectoryPath);
                if(createResult != DocNetStatus.Success) return createResult;

                var globalNamespace = generateGlobalNamespaceModel(inputPaths, mode);
                
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

        /// <summary>
        /// Generates documentation using the provided assembly files.
        /// </summary>
        /// <returns>The program's exit status code.</returns>
        public DocNetStatus ExecuteAssemblyParser(IEnumerable<AssemblyXmlPair> inputPairs, string outputPath, OutputMode mode = OutputMode.PublicOnly)
        {
            if (inputPairs == null)
                throw new ArgumentNullException("input pairs");
            if (outputPath == null)
                throw new ArgumentNullException("output path");

            Log.Info(String.Empty);
            Log.Info("DocNet started...");

            try
            {
                var outputDirectoryPath = Path.Combine(outputPath, _rootDirectoryName);

                // Delete existing output directory
                var purgeResult = PurgeOutputDirectory(outputDirectoryPath);
                if (purgeResult != DocNetStatus.Success) return purgeResult;

                // Create output directory
                var createResult = CreateOutputDirectory(outputDirectoryPath);
                if (createResult != DocNetStatus.Success) return createResult;

                var globalNamespace = GenerateGlobalNamespaceModelFromAssembly(inputPairs, mode);

                // Generate documentation
                Log.Info("\nGenerating documentation...");
                _documentationGenerator.GenerateDocumentation(globalNamespace, outputDirectoryPath);

                Log.Info("Documentation generation complete.");
                Log.Info(String.Empty);
                Log.Info("Thank you for using DocNet!");
                return DocNetStatus.Success;
            }
            catch (ConfigurationException ex)
            {
                Log.Debug(ex);
                Log.Fatal(ex.Message);
                return ex.Status;
            }
            catch (InvalidFileTypeException ex)
            {
                Log.Debug(ex);
                return DocNetStatus.InvalidInputPath;
            }
            catch (CsParsingException ex)
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
                _csSourceParser.ParseIntoNamespace(csFile, globalNamespace, mode);
            }
        }

        private void ParseCsAssemblyFile(string assemblyFilePath, string xmlFilePath, GlobalNamespaceModel globalNamespace, OutputMode mode)
        {
            var assemblyFile = Assembly.LoadFrom(assemblyFilePath);
            using (var xmlFile = File.OpenRead(xmlFilePath))
            {
                _csAssemblyParser.ParseIntoNamespace(assemblyFile, xmlFile, globalNamespace, mode);
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
            return csFiles;
        }

        private IList<AssemblyXmlPair> validateFileExtensionsForAssemblyXmlPairs(IEnumerable<AssemblyXmlPair> inputPairs)
        {
            IList<AssemblyXmlPair> assemblyXmlPair = new List<AssemblyXmlPair>();
            foreach (var inputPair in inputPairs)
            {
                string assemblyFileExtension = Path.GetExtension(inputPair.AssemblyFilePath);
                if (!assemblyFileExtension.Equals(".dll") && !assemblyFileExtension.Equals(".exe"))
                {
                    Log.ErrorFormat(CultureInfo.CurrentCulture,
                            "Input path \"{0}\" is an invalid file type for an assembly file.", inputPair.AssemblyFilePath);
                    throw new InvalidFileTypeException(inputPair.AssemblyFilePath);
                }
                string xmlFileExtension = Path.GetExtension(inputPair.XmlFilePath);
                if (!xmlFileExtension.Equals(".xml"))
                {
                    Log.ErrorFormat(CultureInfo.CurrentCulture,
                            "Input path \"{0}\" is an invalid file type for an XML file.", inputPair.XmlFilePath);
                    throw new InvalidFileTypeException(inputPair.XmlFilePath);
                }
                assemblyXmlPair.Add(inputPair);
            }
            return assemblyXmlPair;
        }

        private IList<T> RemoveDuplicates<T>(IList<T> input)
        {
            var uniqueInput = input.Distinct().ToList();
            if (uniqueInput.Count() != input.Count)
            {
                var duplicates = input.Except(uniqueInput);
                foreach (var duplicate in duplicates)
                {
                    Log.WarnFormat(CultureInfo.CurrentCulture,
                        "Duplicate input file \"{0}\" will be skipped.", duplicate);
                }
            }
            return uniqueInput;
        }

        private IList<AssemblyXmlPair> RemoveDuplicateAssemblyXmlPairs(IList<AssemblyXmlPair> inputPairs)
        {
            var uniquePairs = inputPairs.Distinct().ToList();
            if (uniquePairs.Count() != inputPairs.Count)
            {
                var duplicates = inputPairs.Except(uniquePairs);
                foreach (var duplicate in duplicates)
                {
                    Log.WarnFormat(CultureInfo.CurrentCulture,
                        "Duplicate input file \"{0}\" will be skipped.", duplicate);
                }
            }
            return uniquePairs;
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

        private GlobalNamespaceModel GenerateGlobalNamespaceModelFromSource(IEnumerable<string> inputPaths, OutputMode mode)
        {
            // Get list of unque input files
            var csFileList = GetCsFileList(inputPaths);
            var uniqueInputFileList = RemoveDuplicates(csFileList);
            Log.Info(String.Empty);
            Log.Info("Documentation will be generated using the following input files:");
            foreach (var filePath in uniqueInputFileList)
            {
                Log.Info("    " + filePath);
            }
            Log.Info(String.Empty);

            // Parse each input file
            var globalNamespace = new GlobalNamespaceModel();
            foreach (var filePath in uniqueInputFileList)
            {
                Log.InfoFormat("Parsing \"{0}\".", filePath);
                ParseCsFile(filePath, globalNamespace, mode);
            }
            return globalNamespace;
        }

        private GlobalNamespaceModel GenerateGlobalNamespaceModelFromAssembly(IEnumerable<AssemblyXmlPair> inputPairs, OutputMode mode)
        {
            var pairs = validateFileExtensionsForAssemblyXmlPairs(inputPairs);
            var uniqueInputPairList = RemoveDuplicates(pairs);
            Log.Info(String.Empty);
            Log.Info("Documentation will be generated using the following input files:");
            foreach (var assemblyXmlPair in uniqueInputPairList)
            {
                Log.Info("    " + assemblyXmlPair.AssemblyFilePath + " = " + assemblyXmlPair.XmlFilePath);
            }
            Log.Info(String.Empty);

            // Parse each input file
            var globalNamespace = new GlobalNamespaceModel();
            foreach (AssemblyXmlPair filePath in uniqueInputPairList)
            {
                Log.InfoFormat("Parsing \"{0}\".", filePath.AssemblyFilePath);
                ParseCsAssemblyFile(filePath.AssemblyFilePath, filePath.XmlFilePath, globalNamespace, mode);
            }
            return globalNamespace;
        }

        #endregion
    }
}
