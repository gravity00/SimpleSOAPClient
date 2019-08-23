namespace SimpleSOAPClient
{
    /// <summary>
    /// Represents a class with <see cref="SoapRequestSettings"/>.
    /// </summary>
    public interface IHaveSoapRequestSettings
    {
        /// <summary>
        /// SOAP request settings
        /// </summary>
        SoapRequestSettings Settings { get; }
    }
}