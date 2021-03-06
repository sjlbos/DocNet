﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// <summary>
    /// The entry point class of the DocNet command line tool.
    /// </summary>
    /// <remarks>
    /// This class provides a thin wrapper around an <see cref="IDocNetController"/> instance, allowing the controller
    /// to be executed as a command line application.
    /// </remarks>
    public class Program
    {
        #region Main

        /// <summary>
        /// The entry point into the DocNet command line tool.
        /// </summary>
        /// <param name="args">
        ///     An array of program arguments. Seethe <see cref="ProgramArguments"/> class for more detailed
        ///     information on expected program arguments.
        /// </param>
        /// <returns>A DocNet status code.</returns>
        public static int Main(string[] args)
        {
            var programArgs = new ProgramArguments();
            
            if (!Parser.Default.ParseArguments(args, programArgs))
            {
                if (programArgs.HelpSpecified)
                {
                    Log.Info(GetHelpMessage(programArgs));
                    return (int) DocNetStatus.Success;
                }

                Log.Error("Invalid arguments.");
                Log.Error(GetHelpMessage(programArgs));
                return (int) DocNetStatus.InvalidProgramArguments;
            }

            if(programArgs.HelpSpecified)
            {
                Log.Info(GetHelpMessage(programArgs));
                return (int) DocNetStatus.Success;
            }

            programArgs.InputPaths = RemoveInitialWhitespacefromPaths(programArgs.InputPaths);
                
            if (programArgs.DirectoryModeSpecified)
                programArgs.InputPaths = GetCsFileListFromDirectoryList(programArgs.InputPaths, programArgs.UseRecursiveSearch);

            var program = new Program();
            int status = (int)program.Run(programArgs);
            System.Console.ReadLine();
            return status;
        }

        #endregion

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private const string DefaultRootDirectoryName = "DocNet";

        private readonly DocNetController _controller;

        public Program()
        {
            var exportedFileConfig = (FileListConfigurationSection)ConfigurationManager.GetSection("exportedMarkupFiles");
            var exportedFileList = new List<string>();
            foreach(var fileElement in exportedFileConfig.Files)
            {
                exportedFileList.Add(((FileElement)fileElement).Name);
            }

            var projectParser = new ProjectParser();
            var config = new ControllerConfiguration
            {
                SolutionParser = new OnionSolutionParserWrapper(projectParser),
                ProjectParser = projectParser,
                CsParser = new CsTextParser(),
                DocumentationGenerator = new HtmlDocumentationGenerator(exportedFileList), 
                RootDirectoryName = ConfigurationManager.AppSettings["RootDirectoryName"] ?? DefaultRootDirectoryName,
            };

            _controller = new DocNetController(config);
        }

        /// <summary>
        /// Runs the DocNet command line program using the specified program arguments.
        /// </summary>
        /// <param name="args">The arguments provided to the program by the user.</param>
        /// <returns>An exit status code.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public DocNetStatus Run(ProgramArguments args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            try
            {
                string currentDirectoryPath = Directory.GetCurrentDirectory();
                var documentationSettings = new DocumentationSettings
                {
                    InputFilePaths = args.InputPaths.Select(p => MakePathAbsolute(currentDirectoryPath, p)),
                    OutputDirectoryPath = MakePathAbsolute(currentDirectoryPath, args.OutputDirectory),
                    OutputMode = GetOutputMode(args)
                };
  
                return _controller.Execute(documentationSettings);
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
                Log.Fatal("An unknown error occured.");
                return DocNetStatus.UnknownFailure;
            }
        }

        #region Helper Methods

        private static OutputMode GetOutputMode(ProgramArguments args)
        {
            return args.DocumentAllElement ? OutputMode.AllElements : OutputMode.PublicOnly;
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

        private static IList<string> RemoveInitialWhitespacefromPaths(IList<string> inputPaths)
        {
            for (int i = 0; i < inputPaths.Count(); i++)
            {
                inputPaths[i] = inputPaths[i].Trim();
            }
            return inputPaths;
        }
    
        #endregion
    }
}
