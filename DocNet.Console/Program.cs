using System.Linq;
using log4net;
using CommandLine;
using System.IO;
using DocNet.Core;
using DocNet.Core.Output;
using DocNet.Core.Output.Html;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Parsers.VisualStudio;
using DocNetController = DocNet.Core.DocNetController;


namespace DocNet.Console
{
    
   /* public enum ClStatus
    {    
        Success,
        Help,
        KnownFailure,
        UnknownFailure,
        InvalidInputPath,
        UnreachableInputPath,
        InvalidOutputPath,
        UnreachableOutputPath
    } */


    public class Program
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        private readonly ILog _log;
        private readonly ISolutionParser _solutionParser;
        private readonly IProjectParser _projectParser;
        private readonly ICsParser _csParser;
        private readonly IDocumentationGenerator _documentationGenerator;

        public Program()
        {
            ILog logger = LogManager.GetLogger(typeof (DocNetController));
            IProjectParser projectParser = new ProjectParser();
            ISolutionParser solutionParser = new OnionSolutionParserWrapper(projectParser);
            ICsParser csParser = new CsTextParser();
            IDocumentationGenerator documentationGenerator = new HtmlDocumentationGenerator();

            _log = logger;
            _solutionParser = solutionParser;
            _projectParser = projectParser;
            _csParser = csParser;
            _documentationGenerator = documentationGenerator;
        }

        //Command Line Options
        class Options
        {
            [Option('i', "input", Required = true, HelpText = "File/Folder to convert to documentation.")]
            public string InputFile { get; set; }
            [Option('o', "output", Required = true, HelpText = "Location to output documentation.")]
            public string OutputFile { get; set; }
            [Option('r', "recurse", HelpText = "Recursively search through input folder", DefaultValue = false)]
            public bool RecurseOption { get; set; }
            [Option('h', "help", HelpText = "Display command line help information", Required = false, DefaultValue = false)]
            public bool Help { get; set; }
        }

        //Checks that command line input is correct and outputs a custom message depending on whats missing
        static private bool CmdCheck(string[] args)
        {
            //Determine which required commands are missing
            if (!args.Any())
            {
                Log.Error("No commands found. Use the following commands:");
                return false;
            }
            //Check if args contains --help
            if (args.Contains("-h") || args.Contains("--help"))
            {
                return false;
            }
            //Check if required commands exist !(((Array.IndexOf(args, "-o") >= 0) || (Array.IndexOf(args, "--output") >= 0)) && ((Array.IndexOf(args, "-i") >= 0) || (Array.IndexOf(args, "--input") >= 0)))
            if (!((args.Contains("-o") || args.Contains("--output")) && (args.Contains("-i") || args.Contains("--input"))))
            {
                Log.Error("Required commands not found. Use the following commands:");
                return false;
            }
            else
            {
                return true;
            }
        }
        //Check if input is file, directory or neither
        private static string PathCheck(string path)
        {
            //If file exists check the file extension
            if (File.Exists(path))
            {
                string extension = Path.GetExtension(path);
                if (extension == null)
                {
                    Log.ErrorFormat("{0}'s file extension is NULL", path);
                    return "0";
                }
                else if (extension.Equals(".sln"))
                {
                    return ".sln";
                }
                else if (extension.Equals(".csproj"))
                {
                    return ".csproj";
                }
                else if (extension.Equals(".cs"))
                {
                    return ".cs";
                }
                else
                {
                    Log.Error("Input file type is invalid. Please input a directory, .cs file, .sln file or .csproj file");
                    return "0";
                }

            }
            //Check if directory exists
            else if (Directory.Exists(path))
            {
                return "directory";
            }
            //path is not a valid file or directory
            else
            {
                Log.ErrorFormat("{0} is not a valid file or directory.", path);
                return "0";
            }

        }

        //Get the .CS file location from a solution file, csproj file or directory
        public DocNetStatus GetCsFiles(string inputFile, string outputFile, bool recurseOption)
        {
            DocNetStatus parserStatus;
            IDocNetController docnet = new DocNetController(_log, _solutionParser, _projectParser, _csParser, _documentationGenerator);
            string[] filelist;

            //Check path 1 is file 2 is directory 0 is neither
            var extension = PathCheck(inputFile);

                //If input file is .cs return only the .cs path
            if (extension.Equals(".cs"))
            {
                //nolist[0] = inputFile;
                filelist = new string[1];
                filelist[0] = inputFile;
                parserStatus = docnet.DocumentCsFiles(outputFile, filelist);
                return parserStatus;
            }
            //If input file is .sln get .cs file list from solution file
            else if (extension.Equals(".sln"))
            {
                parserStatus = docnet.DocumentSolution(outputFile, inputFile);

               return parserStatus;
            }
            //If input file is .csproj get .cs file list from project file
            else if (extension.Equals(".csproj"))
            {
                parserStatus = docnet.DocumentCsProject(outputFile, inputFile);

                return parserStatus;
           }
           //if path is directory
           else if (extension.Equals("directory"))
           {   
                if (recurseOption)
                {    
                     //Recursively search directory for .cs files and return a list
                     filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.AllDirectories);
                     if (filelist.Length > 0)
                     {
                         parserStatus = docnet.DocumentCsFiles(outputFile, filelist);
                         return parserStatus;
                     }
                     else
                     {
                         Log.ErrorFormat("No .cs files found in {0}", inputFile);
                         return DocNetStatus.Success;
                     }
                }
                else
                {
                    //Search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.TopDirectoryOnly);
                    if (filelist.Length > 0)
                    {
                        parserStatus = docnet.DocumentCsFiles(outputFile, filelist);
                        return parserStatus;
                    }
                    else
                    {
                        Log.ErrorFormat("No .cs files found in {0}", inputFile);
                        return DocNetStatus.Success;
                    }
                }
            }    
            //if path is not file or directory
            else
            {
                return DocNetStatus.InvalidInputPath;
            }   

        }     
        //Parses command line
        static public DocNetStatus ParseArguments(string[] args)
        {
            var options = new Options();

            //Parse CL Input
            if (Parser.Default.ParseArguments(args, options))
            {
                //Display help and return
                if (options.Help)
                {
                    Log.Info(CommandLine.Text.HelpText.AutoBuild(options));
                    System.Console.ReadLine();
                    return DocNetStatus.Success;
                }
                else
                {
                    //string[] csfilelist;
                    //Double check that commands are valid
                    if (!CmdCheck(args))
                    {
                        Log.Info(CommandLine.Text.HelpText.AutoBuild(options));
                        return DocNetStatus.Success;
                    }

                    //Get the .CS files contained in the file or directory
                    Program getFiles = new Program();
                    DocNetStatus status = getFiles.GetCsFiles(options.InputFile, options.OutputFile, options.RecurseOption);
                    return status;
                }
            }
            //Commands not valid
            else
            {
                //Check why commands aren't valid
                if (!CmdCheck(args))
                {
                    Log.Info(CommandLine.Text.HelpText.AutoBuild(options));
                    return DocNetStatus.Success;
                }
                //Check if input/output are valid
                else
                {
                    if (string.IsNullOrEmpty(options.InputFile) || string.IsNullOrEmpty(options.OutputFile))
                    {
                        Log.Error("No arguments found for input and/or output");
                    }
                }
            }
            return DocNetStatus.Success;
        }

        //Read Status passed by Parsers
        static void StatusResults(DocNetStatus parseStatus)
        {
            //Print info message based on status
            if (parseStatus == DocNetStatus.Success)
            {
                Log.Info("DocNet has returned successfully!");
            }
            else if (parseStatus == DocNetStatus.UnknownFailure)
            {
                Log.Info("DocNet has failed with an unknown error");
            }
            else if (parseStatus == DocNetStatus.InvalidInputPath)
            {
                Log.Info("DocNet has failed with an invalid input path");
            }
            else if (parseStatus == DocNetStatus.UnreachableInputPath)
            {
                Log.Info("DocNet has failed because the input path was unreachable");
            }
             else if (parseStatus == DocNetStatus.InvalidOutputPath)
            {
                Log.Info("DocNet has failed with an invalid output path");
            }
              else if (parseStatus == DocNetStatus.UnreachableOutputPath)
            {
                Log.Info("DocNet has failed because the output path was unreachable");
            }
        }

        static void Main(string[] args)
        {
            Log.Info("Welcome to DocNet!");
            
            //TODO Test Code REMOVE WHEN DONE
            var i=0;
            foreach(var arg in args)
            {
                Log.InfoFormat("Arg[{0}] = [{1}]", i, arg);
                i++;
            }
            //TODO END TEST CODE

            //Parse Arguments and return list of .cs files.
            DocNetStatus parseStatus = ParseArguments(args);

            StatusResults(parseStatus);

            System.Console.ReadLine();
        }
    }
}
