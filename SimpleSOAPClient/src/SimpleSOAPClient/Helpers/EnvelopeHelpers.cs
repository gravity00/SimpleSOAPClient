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

    public static class EnvelopeHelpers
    {
        public static SoapEnvelope WithBody<T>(this SoapEnvelope envelope, T body)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (envelope.Body == null)
                envelope.Body = new SoapEnvelopeBody();

            envelope.Body.Value = body.ToXElement();

            return envelope;
        }

        public static SoapEnvelope WithHeaders(
            this SoapEnvelope envelope, Func<ICollection<XElement>, ICollection<XElement>> builder)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            ICollection<XElement> headers;
            if (envelope.Header == null)
            {
                envelope.Header = new SoapEnvelopeHeader();
                headers = new List<XElement>();
            }
            else
            {
                headers = new List<XElement>(envelope.Header.Headers);
            }
            envelope.Header.Headers = builder(headers).ToArray();

            return envelope;
        }

        public static bool IsFaulted(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            var body = envelope.Body?.Value;
            return body != null &&
                   Constant.Namespace.OrgXmlSoapSchemasSoapEnvelope.Equals(
                       body.Name.NamespaceName, StringComparison.InvariantCultureIgnoreCase) &&
                   "fault".Equals(body.Name.LocalName, StringComparison.InvariantCultureIgnoreCase);
        }

        public static void ThrowIfFaulted(this SoapEnvelope envelope)
        {
            if (envelope == null) throw new ArgumentNullException(nameof(envelope));

            if (!envelope.IsFaulted()) return;

            var fault = envelope.Body.Value.ToObject<SoapFault>();
            throw new FaultException
            {
                Code = fault.Code,
                String = fault.String,
                Actor = fault.Actor,
                Detail = fault.Detail
            };
        }
    }
}
