namespace SimpleSOAPClient.Helpers
{
    using System;
    using System.Net.Http;
    using Handlers;
    using Models;

    /// <summary>
    /// Helper methods for handling results
    /// </summary>
    public static class Handling
    {
        #region RequestEnvelopeHandler

        /// <summary>
        /// Cancels the request envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestEnvelopeHandlerResult CancelRequestEnvelopeHandlerFlow(IRequestEnvelopeHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new RequestEnvelopeHandlerResult(true, data.Envelope);
        }

        /// <summary>
        /// Cancels the request envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="envelope">The resultant SOAP envelope</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestEnvelopeHandlerResult CancelRequestEnvelopeHandlerFlow(SoapEnvelope envelope)
        {
            return new RequestEnvelopeHandlerResult(true, envelope);
        }

        /// <summary>
        /// Proceeds the request envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestEnvelopeHandlerResult ProceedRequestEnvelopeHandlerFlowWith(IRequestEnvelopeHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new RequestEnvelopeHandlerResult(false, data.Envelope);
        }

        /// <summary>
        /// Proceeds the request envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="envelope">The resultant SOAP envelope</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestEnvelopeHandlerResult ProceedRequestEnvelopeHandlerFlowWith(SoapEnvelope envelope)
        {
            return new RequestEnvelopeHandlerResult(false, envelope);
        }

        #endregion

        #region RequestRawHandler

        /// <summary>
        /// Cancels the request raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestRawHandlerResult CancelRequestRawHandlerFlow(IRequestRawHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new RequestRawHandlerResult(true, data.Request, data.Content);
        }

        /// <summary>
        /// Cancels the request raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="request">The resultant HTTP request</param>
        /// <param name="content">The resultant string context</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestRawHandlerResult CancelRequestRawHandlerFlow(HttpRequestMessage request, string content)
        {
            return new RequestRawHandlerResult(true, request, content);
        }

        /// <summary>
        /// Proceeds the request raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestRawHandlerResult ProceedRequestRawHandlerFlowWith(IRequestRawHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new RequestRawHandlerResult(false, data.Request, data.Content);
        }

        /// <summary>
        /// Proceeds the request raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="request">The resultant HTTP request</param>
        /// <param name="content">The resultant string context</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IRequestRawHandlerResult ProceedRequestRawHandlerFlowWith(HttpRequestMessage request, string content)
        {
            return new RequestRawHandlerResult(false, request, content);
        }

        #endregion

        #region ResponseRawHandler

        /// <summary>
        /// Cancels the response raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseRawHandlerResult CancelResponseRawHandlerFlow(IResponseRawHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new ResponseRawHandlerResult(true, data.Response, data.Content);
        }

        /// <summary>
        /// Cancels the response raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="response">The resultant HTTP request</param>
        /// <param name="content">The resultant string context</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseRawHandlerResult CancelResponseRawHandlerFlow(HttpResponseMessage response, string content)
        {
            return new ResponseRawHandlerResult(true, response, content);
        }

        /// <summary>
        /// Proceeds the response raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseRawHandlerResult ProceedResponseRawHandlerFlowWith(IResponseRawHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new ResponseRawHandlerResult(false, data.Response, data.Content);
        }

        /// <summary>
        /// Proceeds the response raw handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="response">The resultant HTTP request</param>
        /// <param name="content">The resultant string context</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseRawHandlerResult ProceedResponseRawHandlerFlowWith(HttpResponseMessage response, string content)
        {
            return new ResponseRawHandlerResult(false, response, content);
        }

        #endregion

        #region ResponseEnvelopeHandler

        /// <summary>
        /// Cancels the response envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseEnvelopeHandlerResult CancelResponseEnvelopeHandlerFlow(IResponseEnvelopeHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new ResponseEnvelopeHandlerResult(true, data.Envelope);
        }

        /// <summary>
        /// Cancels the response envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to true
        /// </summary>
        /// <param name="envelope">The resultant SOAP envelope</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseEnvelopeHandlerResult CancelResponseEnvelopeHandlerFlow(SoapEnvelope envelope)
        {
            return new ResponseEnvelopeHandlerResult(true, envelope);
        }

        /// <summary>
        /// Proceeds the response envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="data">The data to use</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseEnvelopeHandlerResult ProceedResponseEnvelopeHandlerFlowWith(IResponseEnvelopeHandlerData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            return new ResponseEnvelopeHandlerResult(false, data.Envelope);
        }

        /// <summary>
        /// Proceeds the response envelope handlers flow with <see cref="IHandlerResult.CancelHandlerFlow"/> set to false
        /// </summary>
        /// <param name="envelope">The resultant SOAP envelope</param>
        /// <returns>The handler result</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IResponseEnvelopeHandlerResult ProceedResponseEnvelopeHandlerFlowWith(SoapEnvelope envelope)
        {
            return new ResponseEnvelopeHandlerResult(false, envelope);
        }

        #endregion
    }
}