namespace SimpleSOAPClient.Handlers
{
    /// <summary>
    /// Base class for handler results
    /// </summary>
    public abstract class HandlerResult : IHandlerResult
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="cancelHandlerFlow">Cancel the current handler flow?</param>
        protected HandlerResult(bool cancelHandlerFlow)
        {
            CancelHandlerFlow = cancelHandlerFlow;
        }

        /// <summary>
        /// Cancel the current handler flow?
        /// </summary>
        public bool CancelHandlerFlow { get; }
    }
}