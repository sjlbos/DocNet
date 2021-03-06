﻿using DocNet.Core.Models.VisualStudio;

namespace DocNet.Core.Parsers.VisualStudio
{
    public interface ISolutionParser
    {
        SolutionModel ParseSolutionFile(string solutionPath);
    }
}
