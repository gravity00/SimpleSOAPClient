namespace SimpleSOAPClient.Helpers
{
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public static class XmlHelpers
    {
        private static readonly XmlSerializerNamespaces EmptyXmlSerializerNamespaces;

        static XmlHelpers()
        {
            EmptyXmlSerializerNamespaces = new XmlSerializerNamespaces();
            EmptyXmlSerializerNamespaces.Add("", "");
        }

        public static string ToXmlString<T>(this T item)
        {
            using (var textWriter = new StringWriter())
            {
                new XmlSerializer(typeof(T))
                    .Serialize(textWriter, item, EmptyXmlSerializerNamespaces);
                var result = textWriter.ToString();

                return result;
            }
        }

        public static XElement ToXElement<T>(this T item)
        {
            return item == null ? null : XElement.Parse(item.ToXmlString());
        }

        public static T ToObject<T>(this string xml)
        {
            using (var textWriter = new StringReader(xml))
            {
                var result = (T)new XmlSerializer(typeof(T)).Deserialize(textWriter);

                return result;
            }
        }

        public static T ToObject<T>(this XElement xml)
        {
            return xml.ToString().ToObject<T>();
        }
    }
}