using log4net;
using CommandLine;

namespace DocNet.Console
{
    using System;
    using System.IO;

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
                Console.WriteLine("No commands found. Use the following commands:");
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
                Console.WriteLine("Required commands not found. Use the following commands:");
                return false;
            }
            else
            {
                return true;
            }
        }

        //Get the .CS files contained in the file or directory
        static public string[] GetCSFiles(string inputFile, bool recurseOption)
        {
            string[] filelist;
            string[] nulllist = new string[1];
            nulllist[0] = "-1";

            //Check path
            if (File.Exists(inputFile))
            {
                // This path is a file
                Console.WriteLine("FILE");
                //TODO Pass to some function to handle file

                //TODO RETURN STATEMENT
                return nulllist;
            }
            else if (Directory.Exists(inputFile))
            {
                // This path is a directory
                Console.WriteLine("Directory");
                if (recurseOption)
                {
                    //Recursively search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.AllDirectories);
                    return filelist;
                }
                else
                {
                    //Search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.TopDirectoryOnly);
                    return filelist;
                }
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", inputFile);
                return nulllist;
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
                    Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                    Console.ReadLine();
                    return;
                }
                else
                {
                    string[] csfilelist;
                    //Double check that commands are valid
                    if (!CmdCheck(args))
                    {
                        Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                        //TODO Console.ReadLine();
                        return;
                    }
                    //TODO Test Code. Will Remove later
                    Console.WriteLine("Input: {0}", options.InputFile);
                    Console.WriteLine("Output: {0}", options.OutputFile);
                    //TODO END TEST CODE

                    //Get the .CS files contained in the file or directory
                    csfilelist = GetCSFiles(options.InputFile, options.RecurseOption);
                    //TODO TEST CODE REMOVE LATER
                    foreach (var csfile in csfilelist)
                    {
                        Console.WriteLine("CSPath: {0}", csfile);
                    }
                    //TODO END TEST CODE

                    //return csfilelist;
                }
            }
            //Commands not valid
            else
            {
                //Check why commands aren't valid
                if (!CmdCheck(args))
                {
                    Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                    //TODO Console.ReadLine();
                    return;
                }
                //Check if input/output are valid
                else
                {
                    if (string.IsNullOrEmpty(options.InputFile) || string.IsNullOrEmpty(options.OutputFile))
                    {
                        Console.WriteLine("No arguments found for input and/or output");
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
                Console.WriteLine("Arg[{0}] = [{1}]", i, arg);
                i++;
            }
            //TODO END TEST CODE

            ParseArguments(args);
             
            Console.ReadLine();
        }
    }
}
