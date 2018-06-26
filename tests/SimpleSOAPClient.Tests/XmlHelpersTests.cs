using SimpleSOAPClient.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;

namespace SimpleSOAPClient.Tests
{
    public class XmlHelpersTests
    {
        const string SERIALIZED_XML = @"<XmlModel xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""urn:simplesoapclient:test""><String>foo</String><Int>42</Int><Bool>true</Bool><Array><string>One</string><string>Two</string><string>Three</string></Array></XmlModel>";
        const string SERIALIZED_DATACONTRACT = @"<DataContractModel xmlns=""urn:simplesoapclient:test"" xmlns:i=""http://www.w3.org/2001/XMLSchema-instance""><String>foo</String><Int>42</Int><Bool>true</Bool><Array xmlns:a=""http://schemas.microsoft.com/2003/10/Serialization/Arrays""><a:string>One</a:string><a:string>Two</a:string><a:string>Three</a:string></Array></DataContractModel>";

        readonly XmlModel _xmlModel = new XmlModel
        {
            String = "foo",
            Bool = true,
            Int = 42,
            Array = new[] { "One", "Two", "Three" }
        };

        readonly DataContractModel _dataContractModel = new DataContractModel
        {
            String = "foo",
            Bool = true,
            Int = 42,
            Array = new[] { "One", "Two", "Three" }
        };

        [Fact]
        public void ToObject_XmlSerializer()
        {
            var result = XmlHelpers.ToObject<XmlModel>(SERIALIZED_XML);
            Assert.Equal("foo", result.String);
            Assert.True(result.Bool);
            Assert.Equal(42, result.Int);
            Assert.Equal(3, result.Array.Length);
        }

        [Fact]
        public void ToObject_DataContractSerializer()
        {
            var result = XmlHelpers.ToObject<DataContractModel>(SERIALIZED_DATACONTRACT);
            Assert.Equal("foo", result.String);
            Assert.True(result.Bool);
            Assert.Equal(42, result.Int);
            Assert.Equal(3, result.Array.Length);
        }


        [Fact]
        public void ToXElement_XmlSerializer()
        {
            var result = XmlHelpers.ToXElement(_xmlModel);
            var actualXml = result.ToString();
            Assert.Equal(SERIALIZED_XML, actualXml, new XmlEqualityComparer());
        }

        [Fact]
        public void ToXElement_DataContractSerializer()
        {
            var result = XmlHelpers.ToXElement(_dataContractModel);
            var actualXml = result.ToString();
            Assert.Equal(SERIALIZED_DATACONTRACT, actualXml, new XmlEqualityComparer());
        }

        [Fact]
        public void ToXmlString_XmlSerializer()
        {
            var result = XmlHelpers.ToXmlString(_xmlModel);
            Assert.Equal(SERIALIZED_XML, result, new XmlEqualityComparer());
        }

        [Fact]
        public void ToXmlString_DataContractSerializer()
        {
            var result = XmlHelpers.ToXmlString(_dataContractModel);
            Assert.Equal(SERIALIZED_DATACONTRACT, result, new XmlEqualityComparer());
        }

        class XmlEqualityComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => XmlNormalizer.DeepEqualsWithNormalization(XDocument.Parse(x), XDocument.Parse(y), null);

            public int GetHashCode(string obj) => XmlNormalizer.Normalize(XDocument.Parse(obj), null).GetHashCode();
        }
    }
}
