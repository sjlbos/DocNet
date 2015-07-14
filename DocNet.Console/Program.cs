using System.Linq;
using log4net;
using CommandLine;
using System.IO;
using DocNet.Core;
using DocNet = DocNet.Core.DocNet;


namespace DocNet.Console
{
    
    public enum ClStatus
    {    
        Success,
        Help,
        KnownFailure,
        UnknownFailure,
        InvalidInputPath,
        UnreachableInputPath,
        InvalidOutputPath,
        UnreachableOutputPath
    }

    public static class Program
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

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
        static public ClStatus GetCSFiles(string inputFile, string outputFile, bool recurseOption)
        {
            ClStatus parserstatus;
            IDocNetController docnet = new DocNetController();
            string[] filelist;

            //Check path 1 is file 2 is directory 0 is neither
            var extension = PathCheck(inputFile);

                //If input file is .cs return only the .cs path
            if (extension.Equals(".cs"))
            {
                //nolist[0] = inputFile;
                filelist = new string[1];
                filelist[0] = inputFile;
                parserstatus = DocNet.DocumentCSFiles(outputFile, filelist);
                return parserstatus;
            }
            //If input file is .sln get .cs file list from solution file
            else if (extension.Equals(".sln"))
            {
                parserstatus = DocNet.DocumentSolution(outputFile, inputFile);

               return parserstatus;
            }
            //If input file is .csproj get .cs file list from project file
            else if (extension.Equals(".csproj"))
            {
                parserstatus = DocNet.DocumentCsProject(outputFile, inputFile);

                return parserstatus;
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
                         parserstatus = DocNet.DocumentCSFiles(outputFile, filelist);
                         return parserstatus;
                     }
                     else
                     {
                         Log.ErrorFormat("No .cs files found in {0}", inputFile);
                         return ClStatus.KnownFailure;
                     }
                }
                else
                {
                    //Search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.TopDirectoryOnly);
                    if (filelist.Length > 0)
                    {
                        parserstatus = DocNet.DocumentCSFiles(outputFile, filelist);
                        return parserstatus;
                    }
                    else
                    {
                        Log.ErrorFormat("No .cs files found in {0}", inputFile);
                        return ClStatus.KnownFailure;
                    }
                }
            }    
            //if path is not file or directory
            else
            {
                return ClStatus.InvalidInputPath;
            }   

        }     
        //Parses command line
        static public ClStatus ParseArguments(string[] args)
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
                    return ClStatus.Help;
                }
                else
                {
                    //string[] csfilelist;
                    //Double check that commands are valid
                    if (!CmdCheck(args))
                    {
                        Log.Info(CommandLine.Text.HelpText.AutoBuild(options));
                        return ClStatus.KnownFailure;
                    }

                    //Get the .CS files contained in the file or directory
                    ClStatus status = GetCSFiles(options.InputFile, options.OutputFile, options.RecurseOption);
                }
            }
            //Commands not valid
            else
            {
                //Check why commands aren't valid
                if (!CmdCheck(args))
                {
                    Log.Info(CommandLine.Text.HelpText.AutoBuild(options));
                    return ClStatus.KnownFailure;
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
            return ClStatus.KnownFailure;
        }

        //Read Status passed by Parsers
        static void StatusResults(ClStatus parseStatus)
        {
            //Print info message based on status
            if (parseStatus == ClStatus.Success)
            {
                Log.Info("DocNet has succeeded!");
            }
            else if (parseStatus == ClStatus.KnownFailure)
            {
                Log.Info("DocNet has failed to create documentation");
            }
            else if (parseStatus == ClStatus.UnknownFailure)
            {
                Log.Info("DocNet has failed with an unknown error");
            }
            else if (parseStatus == ClStatus.InvalidInputPath)
            {
                Log.Info("DocNet has failed with an invalid input path");
            }
            else if (parseStatus == ClStatus.UnreachableInputPath)
            {
                Log.Info("DocNet has failed because the input path was unreachable");
            }
             else if (parseStatus == ClStatus.InvalidOutputPath)
            {
                Log.Info("DocNet has failed with an invalid output path");
            }
              else if (parseStatus == ClStatus.UnreachableOutputPath)
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
            ClStatus parseStatus = ParseArguments(args);

            StatusResults(parseStatus);

            System.Console.ReadLine();
        }
    }
}
