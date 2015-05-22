
using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using DocNet.Models.Comments.Xml;

namespace DocNet.Models.Comments
{
    public class DocComment : IEquatable<DocComment>
    {
        public static T FromXml<T>(string commentXml) where T:DocComment
        {
            if (commentXml == null)
                throw new ArgumentException("innerXml");

            string xmlCommentString = String.Format(CultureInfo.InvariantCulture,
                "<{0}>{1}</{0}>",
                typeof(T).Name,
                commentXml
                );

            using (XmlReader commentReader = XmlReader.Create(new StringReader(commentXml)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                if (serializer.CanDeserialize(commentReader))
                {
                    return serializer.Deserialize(commentReader) as T;
                }
            }
            return null;
        }

        public SummaryTag Summary { get; set; }
        public RemarksTag Remarks { get; set; }
        public ExampleTag Example { get; set; }
        public PermissionTag Permission { get; set; }
        public SeeAlsoTag SeeAlso { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Summary != null ? Summary.GetHashCode() : 0;
                hashCode = (hashCode*397) ^ (Remarks != null ? Remarks.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Example != null ? Example.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Permission != null ? Permission.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SeeAlso != null ? SeeAlso.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DocComment);
        }

        public bool Equals(DocComment other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Summary == null ? (other.Summary == null) : Summary.Equals(other.Summary)
                && Remarks == null ? (other.Remarks == null) : Remarks.Equals(other.Remarks)
                && Example == null ? (other.Example == null) : Example.Equals(other.Example)
                && Permission == null ? (other.Permission == null) : Permission.Equals(other.Permission)
                && SeeAlso == null ? (other.SeeAlso == null) : SeeAlso.Equals(other.SeeAlso);
        }

        #endregion
    }
}
