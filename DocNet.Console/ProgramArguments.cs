using System.Collections.Generic;
using CommandLine;

namespace DocNet.Console
{
    public class ProgramArguments
    {
        // [-h] [-dr] [-f] [-s] [-p] [output] [input,...]

        [Option('h', "help", HelpText = "Display command line help information", Required = false, DefaultValue = false)]
        public bool HelpSpecified { get; set; }

        [Option('f', "file", HelpText = "Specifies that the program should accept .cs files as input.", Required = false, MutuallyExclusiveSet = "fileMode")]
        public bool FileModeSpecified { get; set; }

        [Option('s', "solution", HelpText = "Specifies that the program should accept .sln files as input.", Required = false, MutuallyExclusiveSet = "solutionMode")]
        public bool SolutionModeSpecified { get; set; }

        [Option('p', "project", HelpText = "Specifies that the program should accept .proj files as input.", Required = false, MutuallyExclusiveSet = "projectMode")]
        public bool ProjectModeSpecified { get; set; }

        [Option('d', "directory", HelpText = "Specifies that the program should accept directories as input.", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool DirectoryModeSpecified { get; set; }

        [Option('r', "recursive", HelpText = "Specifies that the program should recursively search for .cs files.", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool UseRecursiveSearch { get; set; }

        [Option(HelpText = "The output directory where the program will store generated documentation.", Required = true)]
        public string OutputDirectory { get; set; }

        [OptionList(Required = true, HelpText = "Paths to program input files or directories (mode dependent).")]
        public IList<string> InputPaths { get; set; }
    }
}
