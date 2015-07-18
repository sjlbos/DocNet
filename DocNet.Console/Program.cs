using System;
using System.Collections.Generic;
using DocNet.Core.Exceptions;
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
                LogErrorAndExit("Invalid arguments.\n" + GetHelpMessage(programArgs),
                    DocNetStatus.InvalidProgramArguments);

            if (programArgs.HelpSpecified)
                LogHelpMessageAndExit(programArgs);

            if (programArgs.DirectoryModeSpecified)
                programArgs.InputPaths = GetCsFileListFromDirectoryList(programArgs.InputPaths, programArgs.UseRecursiveSearch);

            var program = new Program(programArgs);
            return (int)program.Run();
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private readonly ControllerConfiguration _config;

        public Program(ProgramArguments args)
        {
            var projectParser = new ProjectParser();

            _config = new ControllerConfiguration()
            {
                Logger = LogManager.GetLogger(typeof(DocNetController)),
                SolutionParser = new OnionSolutionParserWrapper(projectParser),
                ProjectParser = projectParser,
                CsParser = new CsTextParser(),
                DocumentationGenerator = new HtmlDocumentationGenerator(),
                OutputDirectoryPath = args.OutputDirectory,
                InputFilePaths = args.InputPaths
            };
        }

        public DocNetStatus Run()
        {
            try
            {
                var controller = new DocNetController(_config);
                return controller.Execute();
            }
            catch (ConfigurationException ex)
            {
                Log.Debug(ex);
                Log.Fatal(ex.Message);
                return ex.Status;
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                Log.Fatal("An unknown error occured.");
                return DocNetStatus.UnknownFailure;
            }
        }

        #region Helper Methods

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
