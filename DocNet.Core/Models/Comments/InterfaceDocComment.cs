using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DocNet.Core.Models.Comments.Xml;

namespace DocNet.Core.Models.Comments
{
    public class InterfaceDocComment : DocComment, IEquatable<InterfaceDocComment>
    {
        [XmlElement("typeparam")]
        public List<TypeParameterTag> TypeParameters { get; set; }

        [XmlElement("exception")]
        public List<ExceptionTag> Exceptions { get; set; }

        public InterfaceDocComment()
        {
            TypeParameters = new List<TypeParameterTag>();
            Exceptions = new List<ExceptionTag>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (TypeParameters != null ? TypeParameters.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Exceptions != null ? Exceptions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InterfaceDocComment);
        }

        public bool Equals(InterfaceDocComment other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                && TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)
                && Exceptions == null ? (other.Exceptions == null) : Exceptions.SequenceEqual(other.Exceptions);
        }

        #endregion
    }
}
