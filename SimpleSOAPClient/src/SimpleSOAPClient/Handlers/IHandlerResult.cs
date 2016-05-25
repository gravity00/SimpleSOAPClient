namespace SimpleSOAPClient.Handlers
{
    /// <summary>
    /// Represents the handler result
    /// </summary>
    public interface IHandlerResult
    {
        /// <summary>
        /// Cancel the current handler flow?
        /// </summary>
        bool CancelHandlerFlow { get; }
    }
}