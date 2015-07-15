using System.Collections.Generic;

namespace DocNet.Core
{
    public interface IDocNetController
    {
        DocNetStatus DocumentSolutions(string outputDirectoryPath, IEnumerable<string> solutionFilePaths);

        DocNetStatus DocumentCsProjects(string outputDirectoryPath, IEnumerable<string> projectFilePaths);

        DocNetStatus DocumentCsFiles(string outputDirectoryPath, IEnumerable<string> csFilePaths);
    }
}
