using System;

namespace DocNet.Models.CSharp
{
    public class ClassModel : ClassAndStructModel, IEquatable<ClassModel>
    {
        public bool IsAbstract { get; set; }
        public bool IsStatic { get; set; }
        public bool IsSealed { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ IsAbstract.GetHashCode();
            hashCode = (hashCode * 397) ^ IsStatic.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassModel);
        }

        public bool Equals(ClassModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                   IsAbstract == other.IsAbstract &&
                   IsStatic == other.IsStatic;
        }

        #endregion
    }
}
