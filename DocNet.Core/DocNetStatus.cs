
namespace DocNet.Core
{
    /// <summary>
    /// Possible status codes emitted by the DocNet program.
    /// </summary>
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
        ParsingError,
        NoDocumentableElements
    }
}
