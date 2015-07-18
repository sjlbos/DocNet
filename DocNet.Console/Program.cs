using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using log4net;
using CommandLine;
using System.IO;
using DocNet.Core;
using DocNet.Core.Output.Html;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;

namespace DocNet.Console
{
    public class Program
    {
        #region Main

        public static int Main(string[] args)
        {
            var programArgs = new ProgramArguments();

            if (!Parser.Default.ParseArguments(args, programArgs))
            {
                LogErrorAndExit("Invalid arguments.\n" + GetHelpMessage(programArgs),
                DocNetStatus.InvalidProgramArguments);
            }

            if (programArgs.HelpSpecified)
                LogHelpMessageAndExit(programArgs);

            var program = new Program(programArgs);
            return (int)program.Run();
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private readonly IDocNetController _controller;
        private readonly ProgramMode _mode;
        private IList<string> _inputPaths;
        private string _outputDirectory;

        public Program(ProgramArguments args)
        {
            _mode = GetProgramMode(args);
            ConvertPathArgumentsToAbsolutePaths(args);

            var projectParser = new ProjectParser();
            _controller = new DocNetController(
                LogManager.GetLogger(typeof(DocNetController)),
                new OnionSolutionParserWrapper(projectParser),
                projectParser,
                new CsTextParser(), 
                new HtmlDocumentationGenerator()
                );
        }

        public DocNetStatus Run()
        {
            try
            {
                Log.Info("Welcome to DocNet!");
                ValidateOutputDirectory();
                ValidateInputPaths();
                switch (_mode)
                {
                    case ProgramMode.FileMode:
                        return _controller.DocumentCsFiles(_outputDirectory, _inputPaths);
                    case ProgramMode.ProjectMode:
                        return _controller.DocumentCsProjects(_outputDirectory, _inputPaths);
                    case ProgramMode.SolutionMode:
                        return _controller.DocumentSolutions(_outputDirectory, _inputPaths);
                    case ProgramMode.SingleDirectoryMode:
                        var fileList = GetCsFileListFromDirectoryList(_inputPaths, false);
                        if (fileList.Any()) 
                            return _controller.DocumentCsFiles(_outputDirectory, fileList);
                        Log.Info("No .cs files found.");
                        return DocNetStatus.Success;   
                    case ProgramMode.RecursiveDirectoryMode:
                        fileList = GetCsFileListFromDirectoryList(_inputPaths, true);
                        if (fileList.Any()) 
                            return _controller.DocumentCsFiles(_outputDirectory, fileList);
                        Log.Info("No .cs files found.");
                        return DocNetStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                LogErrorAndExit("An unknown error occured.", DocNetStatus.UnknownFailure);
            }

            return DocNetStatus.Success;
        }

        #region Helper Methods

        private static ProgramMode GetProgramMode(ProgramArguments args)
        {
            if (args.DirectoryModeSpecified)
                return args.UseRecursiveSearch ? ProgramMode.RecursiveDirectoryMode : ProgramMode.SingleDirectoryMode;
            if (args.FileModeSpecified)
                return ProgramMode.FileMode;
            if(args.ProjectModeSpecified)
                return ProgramMode.ProjectMode;
            if(args.SolutionModeSpecified)
                return ProgramMode.SolutionMode;
            return ProgramMode.SingleDirectoryMode;
        }

        private static IList<string> GetCsFileListFromDirectoryList(IEnumerable<string> inputDirectoryPaths, bool recursiveSearch)
        {
            var fileList = new List<string>();
            foreach (var directoryPath in inputDirectoryPaths)
            {
                fileList.AddRange(Directory.GetFiles(
                    directoryPath, 
                    "*.cs", 
                    recursiveSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
                    ));
            }
            return fileList;
        }

        #region Path Validation

        private void ConvertPathArgumentsToAbsolutePaths(ProgramArguments programArgs)
        {
            if (programArgs.InputPaths == null || !programArgs.InputPaths.Any())
                LogErrorAndExit("No input paths specified.", DocNetStatus.InvalidInputPath);
            
            if (String.IsNullOrWhiteSpace(programArgs.OutputDirectory))
            {
                LogErrorAndExit("No output directory specified.", DocNetStatus.InvalidOutputPath);
            }

            _inputPaths = programArgs.InputPaths.Select(ConvertToAbsolutePath).ToList();

            _outputDirectory = ConvertToAbsolutePath(programArgs.OutputDirectory);
        }

        private static string ConvertToAbsolutePath(string path)
        {
            return !Path.IsPathRooted(path) ? Path.GetFullPath(Directory.GetCurrentDirectory() + "\\" + path) : path;
        }

        private void ValidateOutputDirectory()
        {
            if (String.IsNullOrWhiteSpace(_outputDirectory))
            {
                LogErrorAndExit("No output directory specified.", DocNetStatus.InvalidOutputPath);
            }

            if (!Directory.Exists(_outputDirectory))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Output directory \"{0}\" does not exist.",
                    _outputDirectory),
                    DocNetStatus.InvalidOutputPath);
            }
        }

        private void ValidateInputPaths()
        {
            if (_inputPaths == null || !_inputPaths.Any())
                LogErrorAndExit("No input paths specified.", DocNetStatus.InvalidInputPath);

            switch (_mode)
            {
                case ProgramMode.SingleDirectoryMode:
                case ProgramMode.RecursiveDirectoryMode:
                    foreach(var path in _inputPaths)
                    {
                        ValidateDirectoryPath(path);
                    }
                    break;
                case ProgramMode.FileMode:
                    foreach(var path in _inputPaths)
                    {
                        ValidateCsFilePath(path);
                    }
                    break;
                case ProgramMode.ProjectMode:
                    foreach (var path in _inputPaths)
                    {
                        ValidateProjectFilePath(path);
                    }
                    break;
                case ProgramMode.SolutionMode:
                    foreach(var path in _inputPaths)
                    {
                        ValidateSolutionFilePath(path);
                    }
                    break;
            }
        }

        private static void ValidateDirectoryPath(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input directory \"{0}\" could not be found.", directoryPath), 
                    DocNetStatus.InvalidInputPath);
            }
        }

        private static void ValidateCsFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input file \"{0}\" does not exist.", filePath),
                    DocNetStatus.InvalidInputPath);
            }

            if(!".cs".Equals(Path.GetExtension(filePath)))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input file \"{0}\" is not a .cs file."),
                    DocNetStatus.InvalidInputPath);
            }
        }

        private static void ValidateProjectFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input project \"{0}\" does not exist.", filePath),
                    DocNetStatus.InvalidInputPath);
            }

            if (!".csproj".Equals(Path.GetExtension(filePath)))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input project \"{0}\" is not a .csproj file."),
                    DocNetStatus.InvalidInputPath);
            }
        }

        private static void ValidateSolutionFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input solution \"{0}\" does not exist.", filePath),
                    DocNetStatus.InvalidInputPath);
            }

            if (!".sln".Equals(Path.GetExtension(filePath)))
            {
                LogErrorAndExit(String.Format(CultureInfo.CurrentCulture,
                    "Input solution \"{0}\" is not a .sln file."),
                    DocNetStatus.InvalidInputPath);
            }
        }


        #endregion

        #region Logging

        private static string GetHelpMessage(ProgramArguments args)
        {
            return CommandLine.Text.HelpText.AutoBuild(args);
        }

        private static void LogErrorAndExit(string errorMessage, DocNetStatus returnCode)
        {
            Log.Fatal(errorMessage);   
            Environment.Exit((int) returnCode);
        }

        private static void LogHelpMessageAndExit(ProgramArguments args)
        {
            Log.Info(GetHelpMessage(args));
            Environment.Exit((int)DocNetStatus.Success);
        }

        #endregion  
    
        #endregion
    }
}
