namespace SimpleSOAPClient.Handlers
{
    /// <summary>
    /// Represents an handler data.
    /// </summary>
    public interface IHandlerData
    {
        /// <summary>
        /// The URL being invoked
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The action being invoked
        /// </summary>
        string Action { get; }

        /// <summary>
        /// Cancel handler flow?
        /// </summary>
        bool CancelHandlerFlow { get; set; }
    }
}