
namespace DocNet.Core
{
    public enum DocNetStatus
    {
        Success,
        UnknownFailure,
        InvalidInputPath,
        UnreachableInputPath,
        InvalidOutputPath,
        UnreachableOutputPath,
        InvalidProgramArguments,
        InternalError,
        ParsingError
    }
}
