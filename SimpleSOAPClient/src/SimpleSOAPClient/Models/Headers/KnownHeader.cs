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
namespace SimpleSOAPClient.Models.Headers
{
    using System;
    using Microsoft;
    using Oasis.Security;

    public static class KnownHeader
    {
        public static class Microsoft
        {
            public static ActionSoapHeader Action(string action, bool mustUnderstand = true)
            {
                return new ActionSoapHeader
                {
                    Action = action,
                    MustUnderstand = mustUnderstand ? 1 : 0
                };
            }

            public static ToSoapHeader To(string to, bool mustUnderstand = true)
            {
                return new ToSoapHeader
                {
                    To = to,
                    MustUnderstand = mustUnderstand ? 1 : 0
                };
            }
        }

        public static class Oasis
        {
            public static class Security
            {
                public static UsernameTokenAndPasswordTextSoapHeader UsernameTokenAndPasswordText(
                    string username, string password, bool mustUnderstand = true)
                {
                    var randomId = Guid.NewGuid().ToString("N");

                    return new UsernameTokenAndPasswordTextSoapHeader
                    {
                        Timestamp = new Timestamp
                        {
                            Id = string.Concat("_TS", randomId),
                            Created = DateTime.Now,
                            Expires = DateTime.Now.AddMinutes(15)
                        },
                        UsernameToken = new UsernameTokenWithPasswordText
                        {
                            Id = string.Concat("_UT", randomId),
                            Username = username,
                            Password = new UsernameTokenPasswordText
                            {
                                Value = password
                            }
                        },
                        MustUnderstand = mustUnderstand ? 1 : 0
                    };
                }
            }
        }
    }
}
