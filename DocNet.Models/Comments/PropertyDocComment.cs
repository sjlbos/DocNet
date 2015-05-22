
using System;
using DocNet.Models.Comments.Xml;

namespace DocNet.Models.Comments
{
    public class PropertyDocComment : DocComment, IEquatable<PropertyDocComment>
    {
        public ValueTag ValueTag { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (ValueTag != null ? ValueTag.GetHashCode() : 0);
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
                && ValueTag == null ? (other.ValueTag == null) : ValueTag.Equals(other.ValueTag);
        }

        #endregion
    }
}
