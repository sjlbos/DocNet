using System.Collections.Generic;
using CommandLine;

namespace DocNet.Console
{
    public class ProgramArguments
    {
        // [-h] [-a] [-dr] [output] [input,...]

        [Option('h', "help", HelpText = "Display command line help information", Required = false, DefaultValue = false)]
        public bool HelpSpecified { get; set; }

        [Option('a', "all", HelpText = "The program will document all C# elements, regardless of access modifier or whether or not the element has a documentation comment.", Required = false)]
        public bool DocumentAllElement { get; set; }

        [Option('d', "directory", HelpText = "Specifies that the program should accept directories as input.", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool DirectoryModeSpecified { get; set; }

        [Option('r', "recursive", HelpText = "Specifies that the program should recursively search for .cs files. (used alongside directory flag)", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool UseRecursiveSearch { get; set; }

        [Option('o', "output", HelpText = "The output directory where the program will store generated documentation.", Required = true)]
        public string OutputDirectory { get; set; }

        [OptionList('i', "input", Separator = ',', Required = true, HelpText = "Paths to program input files or directories (mode dependent).")]
        public IList<string> InputPaths { get; set; }
    }
}
