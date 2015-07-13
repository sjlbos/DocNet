using DocNet.Core.Models.VisualStudio;

namespace DocNet.Core.Parsers.VisualStudio
{
    public interface IProjectParser
    {
        ProjectModel ParseProjectFile(string projectFilePath);
    }
}
