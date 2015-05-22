using System;
using System.Collections.Generic;
using System.Linq;
using DocNet.Models.Comments.Xml;

namespace DocNet.Models.Comments
{
    public class MethodDocComment : DocComment, IEquatable<MethodDocComment>
    {
        public IList<ParameterTag> Parameters { get; set; }
        public IList<TypeParameterTag> TypeParameters { get; set; }
        public IList<ExceptionTag> Exceptions { get; set; }
        public ReturnsTag ReturnTagValue { get; set; }

        public MethodDocComment()
        {
            Parameters = new List<ParameterTag>();
            TypeParameters = new List<TypeParameterTag>();
            Exceptions = new List<ExceptionTag>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TypeParameters != null ? TypeParameters.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exceptions != null ? Exceptions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MethodDocComment);
        }

        public bool Equals(MethodDocComment other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                && Parameters == null ? (other.Parameters == null) : Parameters.SequenceEqual(other.Parameters)
                && TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)
                && Exceptions == null ? (other.Exceptions == null) : Exceptions.SequenceEqual(other.Exceptions);
        }

        #endregion
    }
}
