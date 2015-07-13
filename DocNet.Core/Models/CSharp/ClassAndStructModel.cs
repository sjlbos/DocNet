using System;
using System.Collections.Generic;

namespace DocNet.Core.Models.CSharp
{
    public abstract class ClassAndStructModel : InterfaceModel, IEquatable<ClassAndStructModel>
    {
        public IList<ConstructorModel> Constructors { get; set; }
        public IList<CsTypeModel> NestedTypes { get; set; } 

        protected ClassAndStructModel()
        {
            Constructors = new List<ConstructorModel>();
            NestedTypes = new List<CsTypeModel>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();

            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassAndStructModel);
        }

        public bool Equals(ClassAndStructModel other)
        {
            if (other == null) return false;
            if (this == other) return true;

            // List element equality is not checked in order to avoid infinite recursion of equality comparisons
            return base.Equals(other) &&
                   ListsHaveEqualSize(Constructors, other.Constructors) &&
                   ListsHaveEqualSize(NestedTypes, other.NestedTypes);
        }

        #endregion
    }
}
