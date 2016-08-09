#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimpleSOAPClient.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Exceptions;
    using Models;
    using Models.V1Dot1;
    using Models.V1Dot2;
    using SoapEnvelope = Models.V1Dot1.SoapEnvelope;
    using SoapEnvelopeBody = Models.V1Dot1.SoapEnvelopeBody;
    using SoapEnvelopeHeader = Models.V1Dot1.SoapEnvelopeHeader;
    using SoapFault = Models.V1Dot1.SoapFault;

    /// <summary>
    /// Helper methods for working with <see cref="SoapEnvelope"/> instances.
    /// </summary>
    public static class EnvelopeHelpers
    {
        private static readonly XName SoapV1Dot1FaultXName =
            XName.Get("Fault", Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope);

        private static readonly XName SoapV1Dot2FaultXName =
            XName.Get("Fault", Constant.Namespace.OrgW3Www200305SoapEnvelope);

        #region Body

        /// <summary>
        /// Sets the given <see cref="XElement"/> as the envelope body.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to be used.</param>
        /// <param name="body">The <see cref="XElement"/> to set as the body.</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope Body(this SoapEnvelope envelope, XElement body)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (envelope.Body == null)
                envelope.Body = new SoapEnvelopeBody();

            envelope.Body.Value = body;

            return envelope;
        }

        /// <summary>
        /// Sets the given <see cref="XElement"/> as the envelope body.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to be used.</param>
        /// <param name="body">The <see cref="XElement"/> to set as the body.</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope Body(this Models.V1Dot2.SoapEnvelope envelope, XElement body)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (envelope.Body == null)
                envelope.Body = new Models.V1Dot2.SoapEnvelopeBody();

            envelope.Body.Value = body;

            return envelope;
        }

        /// <summary>
        /// Sets the given entity as the envelope body.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to be used.</param>
        /// <param name="body">The entity to set as the body.</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope Body<T>(this SoapEnvelope envelope, T body)
        {
            return envelope.Body(
                SoapClientSettings.Default.SerializationProvider.ToXElement(body));
        }

        /// <summary>
        /// Sets the given entity as the envelope body.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to be used.</param>
        /// <param name="body">The entity to set as the body.</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope Body<T>(this Models.V1Dot2.SoapEnvelope envelope, T body)
        {
            return envelope.Body(
                SoapClientSettings.Default.SerializationProvider.ToXElement(body));
        }

        /// <summary>
        /// Extracts the <see cref="SoapEnvelope.Body"/> as an object of the given type.
        /// </summary>
        /// <typeparam name="T">The type do be deserialized.</typeparam>
        /// <param name="envelope">The <see cref="SoapEnvelope"/></param>
        /// <returns>The deserialized object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FaultV1Dot1Exception">Thrown if the body contains a fault</exception>
        public static T Body<T>(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            envelope.ThrowIfFaulted();

            return SoapClientSettings.Default.SerializationProvider.ToObject<T>(envelope.Body.Value);
        }

        /// <summary>
        /// Extracts the <see cref="Models.V1Dot2.SoapEnvelope.Body"/> as an object of the given type.
        /// </summary>
        /// <typeparam name="T">The type do be deserialized.</typeparam>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/></param>
        /// <returns>The deserialized object</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FaultV1Dot1Exception">Thrown if the body contains a fault</exception>
        public static T Body<T>(this Models.V1Dot2.SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            envelope.ThrowIfFaulted();

            return SoapClientSettings.Default.SerializationProvider.ToObject<T>(envelope.Body.Value);
        }

        #endregion

        #region Headers

        /// <summary>
        /// Appends the received <see cref="XElement"/> collection to the existing
        /// ones in the received <see cref="SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapHeader"/> collection to append</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope WithHeaders(
            this SoapEnvelope envelope, params XElement[] headers)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (headers.Length == 0) return envelope;

            if (envelope.Header == null)
            {
                envelope.Header = new SoapEnvelopeHeader
                {
                    Headers = headers
                };
            }
            else
            {
                var envelopeHeaders = new List<XElement>(envelope.Header.Headers);
                envelopeHeaders.AddRange(headers);
                envelope.Header.Headers = envelopeHeaders.ToArray();
            }

            return envelope;
        }

        /// <summary>
        /// Appends the received <see cref="XElement"/> collection to the existing
        /// ones in the received <see cref="Models.V1Dot2.SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapEnvelopeHeaderBlock"/> collection to append</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope WithHeaders(
            this Models.V1Dot2.SoapEnvelope envelope, params XElement[] headers)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (headers.Length == 0) return envelope;

            if (envelope.Header == null)
            {
                envelope.Header = new Models.V1Dot2.SoapEnvelopeHeader
                {
                    Headers = headers
                };
            }
            else
            {
                var envelopeHeaders = new List<XElement>(envelope.Header.Headers);
                envelopeHeaders.AddRange(headers);
                envelope.Header.Headers = envelopeHeaders.ToArray();
            }

            return envelope;
        }

        /// <summary>
        /// Appends the received <see cref="XElement"/> collection to the existing
        /// ones in the received <see cref="SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapHeader"/> collection to append</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope WithHeaders(
            this SoapEnvelope envelope, IEnumerable<XElement> headers)
        {
            return envelope.WithHeaders(headers as XElement[] ?? headers.ToArray());
        }

        /// <summary>
        /// Appends the received <see cref="XElement"/> collection to the existing
        /// ones in the received <see cref="Models.V1Dot2.SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapEnvelopeHeaderBlock"/> collection to append</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope WithHeaders(
            this Models.V1Dot2.SoapEnvelope envelope, IEnumerable<XElement> headers)
        {
            return envelope.WithHeaders(headers as XElement[] ?? headers.ToArray());
        }

        /// <summary>
        /// Appends the received <see cref="SoapHeader"/> collection to the existing
        /// ones in the received <see cref="SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapHeader"/> collection to append</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope WithHeaders(
            this SoapEnvelope envelope, params SoapHeader[] headers)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (headers.Length == 0) return envelope;
            
            var xElementHeaders = new XElement[headers.Length];
            for (var i = 0; i < headers.Length; i++)
                xElementHeaders[i] = SoapClientSettings.Default.SerializationProvider.ToXElement(headers[i]);

            return envelope.WithHeaders(xElementHeaders);
        }

        /// <summary>
        /// Appends the received <see cref="SoapEnvelopeHeaderBlock"/> collection to the existing
        /// ones in the received <see cref="Models.V1Dot2.SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapEnvelopeHeaderBlock"/> collection to append</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope WithHeaders(
            this Models.V1Dot2.SoapEnvelope envelope, params SoapEnvelopeHeaderBlock[] headers)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            if (headers.Length == 0) return envelope;

            var xElementHeaders = new XElement[headers.Length];
            for (var i = 0; i < headers.Length; i++)
                xElementHeaders[i] = SoapClientSettings.Default.SerializationProvider.ToXElement(headers[i]);

            return envelope.WithHeaders(xElementHeaders);
        }

        /// <summary>
        /// Appends the received <see cref="SoapHeader"/> collection to the existing
        /// ones in the received <see cref="SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapHeader"/> collection to append</param>
        /// <returns>The <see cref="SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapEnvelope WithHeaders(
            this SoapEnvelope envelope, IEnumerable<SoapHeader> headers)
        {
            return envelope.WithHeaders(headers as SoapHeader[] ?? headers.ToArray());
        }

        /// <summary>
        /// Appends the received <see cref="SoapEnvelopeHeaderBlock"/> collection to the existing
        /// ones in the received <see cref="Models.V1Dot2.SoapEnvelope"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to append the headers</param>
        /// <param name="headers">The <see cref="SoapEnvelopeHeaderBlock"/> collection to append</param>
        /// <returns>The <see cref="Models.V1Dot2.SoapEnvelope"/> after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapEnvelope WithHeaders(
            this Models.V1Dot2.SoapEnvelope envelope, IEnumerable<SoapEnvelopeHeaderBlock> headers)
        {
            return envelope.WithHeaders(headers as SoapEnvelopeHeaderBlock[] ?? headers.ToArray());
        }

        /// <summary>
        /// Gets a collection of <see cref="XElement"/> headers by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="XElement"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<XElement> HeadersWithName(this SoapEnvelope envelope, XName name)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Header == null
                ? Enumerable.Empty<XElement>()
                : envelope.Header.Headers.Where(xElement => xElement.Name == name);
        }

        /// <summary>
        /// Gets the first <see cref="XElement"/> header by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="XElement"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<XElement> HeadersWithName(this Models.V1Dot2.SoapEnvelope envelope, XName name)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Header == null
                ? Enumerable.Empty<XElement>()
                : envelope.Header.Headers.Where(xElement => xElement.Name == name);
        }

        /// <summary>
        /// Gets the first <see cref="XElement"/> header by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="XElement"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static XElement HeaderWithName(this SoapEnvelope envelope, XName name)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (envelope.Header == null || envelope.Header.Headers.Length == 0)
                return null;

            return envelope.Header.Headers.FirstOrDefault(xElement => xElement.Name == name);
        }

        /// <summary>
        /// Gets the first <see cref="XElement"/> header by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="XElement"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static XElement HeaderWithName(this Models.V1Dot2.SoapEnvelope envelope, XName name)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (envelope.Header == null || envelope.Header.Headers.Length == 0)
                return null;

            return envelope.Header.Headers.FirstOrDefault(xElement => xElement.Name == name);
        }

        /// <summary>
        /// Gets a given <see cref="SoapHeader"/> by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="SoapHeader"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> HeadersWithName<T>(this SoapEnvelope envelope, XName name)
            where T : SoapHeader
        {
            return
                envelope.HeadersWithName(name)
                    .Select(e => SoapClientSettings.Default.SerializationProvider.ToObject<T>(e));
        }

        /// <summary>
        /// Gets a given <see cref="SoapEnvelopeHeaderBlock"/> by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="SoapEnvelopeHeaderBlock"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> HeadersWithName<T>(this Models.V1Dot2.SoapEnvelope envelope, XName name)
            where T : SoapEnvelopeHeaderBlock
        {
            return
                envelope.HeadersWithName(name)
                    .Select(e => SoapClientSettings.Default.SerializationProvider.ToObject<T>(e));
        }

        /// <summary>
        /// Gets a given <see cref="SoapHeader"/> by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="SoapHeader"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T HeaderWithName<T>(this SoapEnvelope envelope, XName name)
            where T: SoapHeader
        {
            return SoapClientSettings.Default.SerializationProvider.ToObject<T>(envelope.HeaderWithName(name));
        }

        /// <summary>
        /// Gets a given <see cref="SoapEnvelopeHeaderBlock"/> by its <see cref="XName"/>.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> with the headers.</param>
        /// <param name="name">The <see cref="XName"/> to search.</param>
        /// <returns>The <see cref="SoapEnvelopeHeaderBlock"/> or null if not match is found</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T HeaderWithName<T>(this Models.V1Dot2.SoapEnvelope envelope, XName name)
            where T: SoapEnvelopeHeaderBlock
        {
            return SoapClientSettings.Default.SerializationProvider.ToObject<T>(envelope.HeaderWithName(name));
        }

        #endregion

        #region Faulted

        /// <summary>
        /// Does the <see cref="SoapEnvelope.Body"/> contains a fault?
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to validate</param>
        /// <returns>True if a fault exists</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsFaulted(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Body?.Value != null && envelope.Body.Value.Name == SoapV1Dot1FaultXName;
        }

        /// <summary>
        /// Does the <see cref="Models.V1Dot2.SoapEnvelope.Body"/> contains a fault?
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to validate</param>
        /// <returns>True if a fault exists</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsFaulted(this Models.V1Dot2.SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Body?.Value != null && envelope.Body.Value.Name == SoapV1Dot2FaultXName;
        }

        /// <summary>
        /// Extracts the <see cref="SoapEnvelope.Body"/> as a <see cref="SoapFault"/>.
        /// It will fail to deserialize if the body is not a fault. Consider to
        /// use <see cref="IsFaulted(SoapEnvelope)"/> first.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to be used</param>
        /// <returns>The <see cref="SoapFault"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SoapFault Fault(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Body == null
                ? null
                : SoapClientSettings.Default.SerializationProvider.ToObject<SoapFault>(envelope.Body.Value);
        }

        /// <summary>
        /// Extracts the <see cref="Models.V1Dot2.SoapEnvelope.Body"/> as a <see cref="Models.V1Dot2.SoapFault"/>.
        /// It will fail to deserialize if the body is not a fault. Consider to
        /// use <see cref="IsFaulted(Models.V1Dot2.SoapEnvelope)"/> first.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to be used</param>
        /// <returns>The <see cref="SoapFault"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Models.V1Dot2.SoapFault Fault(this Models.V1Dot2.SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            return envelope.Body == null
                ? null
                : SoapClientSettings.Default.SerializationProvider.ToObject<Models.V1Dot2.SoapFault>(envelope.Body.Value);
        }

        /// <summary>
        /// Checks if the <see cref="SoapEnvelope.Body"/> contains a fault 
        /// and throws an <see cref="FaultV1Dot1Exception"/> if true.
        /// </summary>
        /// <param name="envelope">The <see cref="SoapEnvelope"/> to validate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FaultV1Dot1Exception">Thrown if the body contains a fault</exception>
        public static void ThrowIfFaulted(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (!envelope.IsFaulted()) return;

            var fault = envelope.Fault();
            throw new FaultV1Dot1Exception
            {
                Code = fault.Code,
                String = fault.String,
                Actor = fault.Actor,
                Detail = fault.Detail
            };
        }

        /// <summary>
        /// Checks if the <see cref="Models.V1Dot2.SoapEnvelope.Body"/> contains a fault 
        /// and throws an <see cref="FaultV1Dot2Exception"/> if true.
        /// </summary>
        /// <param name="envelope">The <see cref="Models.V1Dot2.SoapEnvelope"/> to validate.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FaultV1Dot2Exception">Thrown if the body contains a fault</exception>
        public static void ThrowIfFaulted(this Models.V1Dot2.SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (!envelope.IsFaulted()) return;

            var fault = envelope.Fault();
            throw new FaultV1Dot2Exception
            {
                Code = fault.Code,
                Reason = fault.Reason,
                Node = fault.Node,
                Role = fault.Role,
                Detail = fault.Detail
            };
        }

        #endregion
    }
}
