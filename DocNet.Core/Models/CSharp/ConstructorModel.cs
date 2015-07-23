using System;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class ConstructorModel : CsElement, INestableElement, IEquatable<ConstructorModel>
    {
        public override string DisplayName
        {
            get
            {
                if (Identifier == null) return null;
                string displayName = Identifier + "(";
                if (Parameters != null)
                    displayName += String.Join(",", Parameters.Select(p => p.TypeName));
                return displayName + ")";
            }
        }

        public override string FullNameQualifier
        {
            get { return Parent != null ? Parent.FullDisplayName : null; }
        }

        public override string FullDisplayName
        {
            get
            {
                if (DisplayName == null) return null;
                if (Parent == null || Parent.FullDisplayName == null) return DisplayName;
                return FullNameQualifier + ":" + DisplayName;
            }
        }

        public override string InternalName
        {
            get
            {
                var internalName = (IsStatic ? "`" : String.Empty) + Identifier; // Extra '`' is to protect against collisions with static and instance constructors
                if (Parameters != null && Parameters.Any())
                    internalName += "`" + String.Join("_", Parameters.Select(p => p.TypeName));
                return internalName;
            }
        }

        public override string FullInternalName
        {
            get
            {
                if (Identifier == null) return null;
                if (Parent != null)
                    return Parent.FullInternalName + "_" + InternalName;
                return Identifier;
            }
        }

        #region INestableElement

        public IParentElement Parent { get; set; }

        public bool IsDirectDescendentOf(IParentElement parent)
        {
            return parent != null && parent == Parent;
        }

        public bool IsDescendentOf(IParentElement parent)
        {
            if (parent == null) return false;
            if (parent == this.Parent) return true;
            var localParentAsChild = this.Parent as INestableElement;
            if (localParentAsChild == null) return false;
            return localParentAsChild.IsDescendentOf(parent);
        }

        #endregion

        public AccessModifier AccessModifier { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public bool IsStatic { get; set; }
        public MethodDocComment DocComment { get; set; }

        public ConstructorModel()
        {
            Parameters = new List<ParameterModel>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ AccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ IsStatic.GetHashCode();
            hashCode = (hashCode * 397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ConstructorModel);
        }

        public bool Equals(ConstructorModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                   AccessModifier == other.AccessModifier &&
                   IsStatic == other.IsStatic &&
                   (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment)) &&
                   (Parameters == null ? (other.Parameters == null) : Parameters.SequenceEqual(other.Parameters));
        }

        #endregion
    }
}
