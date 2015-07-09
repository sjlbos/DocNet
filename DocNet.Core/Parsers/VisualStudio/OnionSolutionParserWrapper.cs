using System;
using System.IO;
using DocNet.Models.VisualStudio;
using Onion.SolutionParser.Parser;

namespace DocNet.Core.Parsers.VisualStudio
{
    public class OnionSolutionParserWrapper : ISolutionParser
    {
        private readonly IProjectParser _projectParser;

        public OnionSolutionParserWrapper(IProjectParser projectParser)
        {
            if(projectParser == null)
                throw new ArgumentNullException("projectParser");
            _projectParser = projectParser;
        }

        public SolutionModel ParseSolutionFile(string solutionPath)
        {
            if (solutionPath == null)
                throw new ArgumentNullException("solutionPath");
            if (!".sln".Equals(Path.GetExtension(solutionPath)))
                throw new IOException("Specified file type is not of type \".sln\".");
            if (!File.Exists(solutionPath))
                throw new FileNotFoundException(solutionPath);

            var parsedSolution = SolutionParser.Parse(solutionPath);
            var solutionModel = new SolutionModel
            {
                Name = Path.GetFileNameWithoutExtension(solutionPath),
                FilePath = solutionPath
            };

            foreach (var project in parsedSolution.Projects)
            {
                string projectPath = Path.Combine(Path.GetDirectoryName(solutionPath) ?? String.Empty, project.Path);
                solutionModel.Projects.Add(_projectParser.ParseProjectFile(projectPath));
            }

            return solutionModel;
        }
    }
}
