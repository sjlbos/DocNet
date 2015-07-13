﻿namespace DocNet.Core
{
    public interface IDocNet
    {
        DocNetStatus DocumentSolution(string outputDirectoryPath, string solutionFilePath);

        DocNetStatus DocumentCsProject(string outputDirectoryPath, string projectFilePath);

        DocNetStatus DocumentCsFiles(string outputDirectoryPath, params string[] csFilePaths);
    }
}
