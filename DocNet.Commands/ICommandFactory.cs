using DocNet.Configuration;

namespace DocNet.Commands
{
    public interface ICommandFactory
    {
        ICommand GetCommandFromConfiguration(DocNetConfig configuration);
    }
}
