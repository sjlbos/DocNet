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

        //Get all .cs files from solution or csproj file
        /*static public string[] ProjectParser(string projectpath, string filetype)
        {
            //TODO TEMP CODE PLEASE IGNORE
            string[] filelist = new string[1];
            filelist[0] = "-1";
            //TODO END TEMP CODE
            return filelist;
        } */

        //Get the .CS files contained in the file or directory
        static public string[] GetCsFiles(string inputFile, string outputFile, bool recurseOption)
        {
            string[] filelist;
            string[] nolist = new string[1];
            nolist[0] = "-1";

            //Check path
            if (File.Exists(inputFile))
            {
                //TODO TEST CODE
                // This path is a file
                Console.WriteLine("FILE");
                //TODO END TEST CODE
                //If input file is .cs return only the .cs path
                if (Path.GetExtension(inputFile).Equals(".cs"))
                {
                    nolist[0] = inputFile;
                    //TODO DocNet.DocumentCSFiles(outputFile, nolist)
                    return nolist;
                }
                //If input file is .sln get .cs file list from solution file
                else if (Path.GetExtension(inputFile).Equals(".sln"))
                {
                    Console.WriteLine("Solution File");
                 
                   //TODO DocNet.DocumentSolution(outputFile, inputFile)
                    //filelist = ProjectParser(inputFile, ".sln");
                    //return filelist;
                    //TODO TEMP RETURN
                    return nolist;
                }
                //If input file is .csproj get .cs file list from project file
                else if (Path.GetExtension(inputFile).Equals(".csproj"))
                {
                    Console.WriteLine("csproj file");
                    //TODO DocNet.DocumentCsProject(outputFile, inputFile)

                    //TODO TEMP RETURN
                    return nolist;
                }
                else
                {
                    Console.WriteLine("Input file type is invalid. Please input a directory, .cs file, .sln file or .csproj file");
                    return nolist;
                }

            }
            else if (Directory.Exists(inputFile))
            {
                //TODO TEST CODE
                // This path is a directory
                Console.WriteLine("Directory");
                //TODO END TEST CODE
                if (recurseOption)
                {
                    //Recursively search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.AllDirectories);
                    if (filelist.Length > 0)
                    {
                        //TODO DocNet.DocumentCSFiles(outputFile, filelist)
                    }
                    else
                    {
                        Console.WriteLine("No .cs files found in {0}", inputFile);
                        return nolist;
                    }
                    return filelist;
                }
                else
                {
                    //Search directory for .cs files and return a list
                    filelist = Directory.GetFiles(inputFile, "*.cs", SearchOption.TopDirectoryOnly);
                    if (filelist.Length > 0)
                    {
                        //TODO DocNet.DocumentCSFiles(outputFile, filelist)
                    }
                    else
                    {
                        Console.WriteLine("No .cs files found in {0}", inputFile);
                        return nolist;
                    }
                    return filelist;
                }
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", inputFile);
                return nolist;
            }   

        }
        //Parses command line
        static public string[] ParseArguments(string[] args)
        {
            var options = new Options();
            string[] nolist = new string[1];
            nolist[0] = "-1";

            //Parse CL Input
            if (Parser.Default.ParseArguments(args, options))
            {
                //Display help and return
                if (options.Help)
                {
                    Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                    Console.ReadLine();
                    return nolist;
                }
                else
                {
                    string[] csfilelist;
                    //Double check that commands are valid
                    if (!CmdCheck(args))
                    {
                        Console.WriteLine(CommandLine.Text.HelpText.AutoBuild(options));
                        //TODO Console.ReadLine();
                        return nolist;
                    }
                    //TODO Test Code. Will Remove later
                    Console.WriteLine("Input: {0}", options.InputFile);
                    Console.WriteLine("Output: {0}", options.OutputFile);
                    //TODO END TEST CODE

                    //Get the .CS files contained in the file or directory
                    csfilelist = GetCsFiles(options.InputFile, options.OutputFile, options.RecurseOption);
                    //TODO TEST CODE REMOVE LATER
                    foreach (var csfile in csfilelist)
                    {
                        Console.WriteLine("CSPath: {0}", csfile);
                    }
                    //TODO END TEST CODE

                    return csfilelist;
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
                    return nolist;
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
            return nolist;
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

            //Parse Arguments and return list of .cs files.
            string[] csfilelist = ParseArguments(args);

            //Check if csfilelist is valid
            //TODO TEMP CODE AS WE ARE NO LONGER PASSING STRINGS
            if(csfilelist[0].Equals("-1"))
            {
                //TODO Test Code REMOVE WHEN DONE
                Console.WriteLine("CSFILELIST IS INVALID");
                Console.ReadLine();
                //TODO END TEST CODE
            }
            else
            {
                //TODO Test Code REMOVE WHEN DONE
                Console.WriteLine("CSFILELIST IS VALID");
                Console.ReadLine();
                //TODO END TEST CODE

                //TODO SEND LIST OF CS FILES TO CSPARSER FUNCTION
            }
             
            //TODO Console.ReadLine();
        }
    }
}
