
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DocNet.Core.Models.Comments.Xml;

namespace DocNet.Core.Models.Comments
{
    public class PropertyDocComment : DocComment, IEquatable<PropertyDocComment>
    {
        [XmlElement("value")]
        public ValueTag Value { get; set; }

        [XmlElement("exception")]
        public List<ExceptionTag> Exceptions { get; set; } 

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Exceptions != null ? Exceptions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PropertyDocComment);
        }

        public bool Equals(PropertyDocComment other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                && Value == null ? (other.Value == null) : Value.Equals(other.Value)
                && Exceptions == null ? (other.Exceptions == null) : Exceptions.SequenceEqual(other.Exceptions);
        }

        #endregion
    }
}
