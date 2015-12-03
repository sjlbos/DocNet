using System.Collections.Generic;
using CommandLine;

namespace DocNet.Console
{
    /// <summary>
    /// A class representing the program arguments available in the DocNet command line.
    /// </summary>
    public class ProgramArguments
    {
        /// <summary>
        /// Indicates whether the '-h' or '--help' flags have been entered by the user.
        /// </summary>
        [Option('h', "help", HelpText = "Display command line help information", Required = false, DefaultValue = false)]
        public bool HelpSpecified { get; set; }

        /// <summary>
        /// Indicates whether the '-a' or '--all' flags have been entered by the user.
        /// </summary>
        [Option('A', "all", HelpText = "The program will document all C# elements, regardless of access modifier or whether or not the element has a documentation comment.", Required = false)]
        public bool DocumentAllElement { get; set; }

        /// <summary>
        /// Indicatess whether the '-d' or '--directory' flags have been specified by the user.
        /// </summary>
        [Option('d', "directory", HelpText = "Specifies that the program should accept directories as input.", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool DirectoryModeSpecified { get; set; }

        /// <summary>
        /// Indicates whether the '-r' or '--recursive' flags have been specified by the user. 
        /// </summary>
        [Option('r', "recursive", HelpText = "Specifies that the program should recursively search for .cs files. (used alongside directory flag)", Required = false, MutuallyExclusiveSet = "directoryMode")]
        public bool UseRecursiveSearch { get; set; }

        /// <summary>
        /// Contains the list of source input files specified by the user.
        /// </summary>
        [OptionList('s', "source", Separator = ',', HelpText = "Paths to program source input files or directories (mode dependent).", MutuallyExclusiveSet = "inputMode")]
        public IList<string> InputSourcePaths { get; set; }

        /// <summary>
        /// Contains the list of assembly/xml input file pairs specified by the user.
        /// </summary>
        [OptionList('a', "assembly", Separator = ',', HelpText = "Paths to program assembly input files or directories (mode dependent).", MutuallyExclusiveSet = "inputMode")]
        public IList<string> InputAssemblyPaths { get; set; }

        /// <summary>
        /// Contains the output directory specified by the user.
        /// </summary>
        [Option('o', "output", HelpText = "The output directory where the program will store generated documentation.", Required = true)]
        public string OutputDirectory { get; set; }
    }
}
