using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace DocNet.Models.Comments.Xml
{
    [Serializable]
    [XmlRoot("comment")]
    public class XmlCommentWrapper
    {
        public static readonly string ElementName = "comment";

        public static XmlCommentWrapper FromInnerXml(string innerXml)
        {
            if (innerXml == null)
                throw new ArgumentException("innerXml");

            string xmlCommentString = String.Format(CultureInfo.InvariantCulture,
                "<{0}>{1}</{0}>",
                ElementName,
                innerXml
                );

            using (XmlReader commentReader = new XmlTextReader(xmlCommentString))
            {
                XmlSerializer serializer = new XmlSerializer(typeof (XmlCommentWrapper));
                if (serializer.CanDeserialize(commentReader))
                {
                    return serializer.Deserialize(commentReader) as XmlCommentWrapper;
                }
            }
            return null;
        }

        public SummaryTag Summary { get; set; }

        [XmlElement("example", typeof(ExampleTag))]
        [XmlElement("exception", typeof(ExceptionTag))]
        [XmlElement("include", typeof(IncludeTag))]
        [XmlElement("param", typeof(ParameterTag))]
        [XmlElement("permission", typeof(PermissionTag))]
        [XmlElement("remarks", typeof(RemarksTag))]
        [XmlElement("returns", typeof(ReturnsTag))]
        [XmlElement("seealso", typeof(SeeAlsoTag))]
        [XmlElement("summary", typeof(SummaryTag))]
        [XmlElement("typeparam", typeof(TypeParameterTag))]
        [XmlElement("value", typeof(ValueTag))]
        public IList<object> Items { get; set; }

        public XmlCommentWrapper()
        {
            Items = new List<object>();
        }
    }
}
