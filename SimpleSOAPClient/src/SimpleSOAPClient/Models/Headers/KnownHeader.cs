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
