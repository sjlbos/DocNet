using System;
using System.Collections.Generic;
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
                Log.Error("Invalid arguments.");
                Log.Error(GetHelpMessage(programArgs));
                return (int)DocNetStatus.InvalidProgramArguments;
            }

            if(programArgs.HelpSpecified)
            {
                Log.Info(GetHelpMessage(programArgs));
                return (int) DocNetStatus.Success;
            }
                
            if (programArgs.DirectoryModeSpecified)
                programArgs.InputPaths = GetCsFileListFromDirectoryList(programArgs.InputPaths, programArgs.UseRecursiveSearch);

            var program = new Program(programArgs);

            int status = (int)program.Run();
            System.Console.ReadLine();
            return status;
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private readonly ControllerConfiguration _config;

        public Program(ProgramArguments args)
        {
            if(args == null)
                throw new ArgumentNullException("args");

            string currentDirectoryPath = Directory.GetCurrentDirectory();

            var projectParser = new ProjectParser();
            _config = new ControllerConfiguration
            {
                SolutionParser = new OnionSolutionParserWrapper(projectParser),
                ProjectParser = projectParser,
                CsParser = new CsTextParser(),
                DocumentationGenerator = new HtmlDocumentationGenerator(),
                OutputDirectoryPath = MakePathAbsolute(currentDirectoryPath, args.OutputDirectory),
                InputFilePaths = args.InputPaths.Select(p => MakePathAbsolute(currentDirectoryPath, p))
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public DocNetStatus Run()
        {
            try
            {
                var controller = new DocNetController(_config);
                return controller.Execute();
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

        private static string MakePathAbsolute(string currentDirectoryPath, string path)
        {
            if(String.IsNullOrWhiteSpace(path)) return path; 
            if(!Path.IsPathRooted(path))
            {
                return Path.GetFullPath(Path.Combine(currentDirectoryPath, path));
            }
            return path;
        }

        private static string GetHelpMessage(ProgramArguments args)
        {
            return CommandLine.Text.HelpText.AutoBuild(args);
        }
    
        #endregion
    }
}
