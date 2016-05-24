namespace SimpleSOAPClient.Handlers
{
    using System;

    /// <summary>
    /// Base class for handler data objects.
    /// </summary>
    public abstract class HandlerData : IHandlerData
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="url">The URL being invoked</param>
        /// <param name="action">The action being invoked</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected HandlerData(string url, string action)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            if (action == null) throw new ArgumentNullException(nameof(action));

            Url = url;
            Action = action;
        }

        /// <summary>
        /// The URL being invoked
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The action being invoked
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Cancel handler flow?
        /// </summary>
        public bool CancelHandlerFlow { get; set; } = false;
    }
}