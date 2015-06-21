using log4net;
using CommandLine;
using System;
using System.IO;

namespace DocNet.Console
{

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
            if (args.Length == 0)
            {
                System.Console.WriteLine("No commands found. Use the following commands:");
                return false;
            }
            //Check if args contains --help
            if ((Array.IndexOf(args, "-h") >= 0) || (Array.IndexOf(args, "--help") >= 0))
            {
                return false;
            }
            //Check if other commands exist
            if (!(((Array.IndexOf(args, "-o") >= 0) || (Array.IndexOf(args, "--output") >= 0)) && ((Array.IndexOf(args, "-i") >= 0) || (Array.IndexOf(args, "--input") >= 0))))
            {
                System.Console.WriteLine("Required commands not found. Use the following commands:");
                return false;
            }
            else
            {
                return true;
            }
        }

        //Check file and then handle file based on if it is a File or Directory
        static public void HandleFile(string checkFile, bool recurseOption)
        {
            //Check path
            if (File.Exists(checkFile))
            {
                // This path is a file
                System.Console.WriteLine("FILE");
                //TODO Pass to some function to handle file
            }
            else if (Directory.Exists(checkFile))
            {
                // This path is a directory
                System.Console.WriteLine("Directory");
                if (recurseOption)
                {
                    //TODO Recursively handle directory
                }
                //TODO Pass to some function to handle directory                      
            }
            else
            {
                System.Console.WriteLine("{0} is not a valid file or directory.", checkFile);
            }   

        }
        //Parses command line
        static public void ParseArguments(string[] args)
        {
            var options = new Options();
            //Parse CL Input
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                //Display help and return
                if (options.Help)
                {
                    System.Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                    System.Console.ReadLine();
                    return;
                }
                else
                {
                    //Double check that commands are valid
                    if (!CmdCheck(args))
                    {
                        System.Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                        //TODO System.Console.ReadLine();
                        return;
                    }
                    //TODO Test Code. Will Remove later
                    System.Console.WriteLine("Input: {0}", options.InputFile);
                    System.Console.WriteLine("Output: {0}", options.OutputFile);

                    //Check file and then handle file based on if it is a File or Directory
                    HandleFile(options.InputFile, options.RecurseOption);
                }
            }
            //Commands not valid
            else
            {
                //Check why commands aren't valid
                if (!CmdCheck(args))
                {
                    System.Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                    //TODO System.Console.ReadLine();
                    return;
                }
                //Check if input/output are valid
                else
                {
                    if (string.IsNullOrEmpty(options.InputFile) || string.IsNullOrEmpty(options.OutputFile))
                    {
                        System.Console.WriteLine("No arguments found for input and/or output");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Log.Info("Welcome to DocNet!");
            
            //TODO Test Code REMOVE WHEN DONE
            var i=0;
            foreach(var arg in args)
            {
                System.Console.WriteLine("Arg[{0}] = [{1}]", i, arg);
                i++;
            }

            ParseArguments(args);
             
            System.Console.ReadLine();
        }
    }
}
