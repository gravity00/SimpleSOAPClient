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
namespace SimpleSOAPClient.Models.V1Dot1.Headers.Oasis.Security
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the password text
    /// </summary>
    [XmlType("Password",
        Namespace = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssWssecuritySecext10)]
    public class UsernameTokenPasswordText
    {
        /// <summary>
        /// The password type
        /// </summary>
        [XmlAttribute("Type", Namespace = "")]
        public string Type { get; set; }

        /// <summary>
        /// The password value
        /// </summary>
        [XmlText]
        public string Value { get; set; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public UsernameTokenPasswordText()
        {
            Type = Constant.Namespace.OrgOpenOasisDocsWss200401Oasis200401WssUsernameTokenProfile10PasswordText;
        }
    }
}
