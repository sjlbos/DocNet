using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class MethodModel : CsElement, INestableElement, IEquatable<MethodModel>
    {
        public override string DisplayName
        {
            get
            {
                if (Identifier == null) return null;
                string displayName = Identifier;
                if (TypeParameters != null && TypeParameters.Any())
                    displayName += "<" + String.Join(",", TypeParameters.Select(p => p.Name)) + ">";
                displayName += "(";
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
                var internalName = Identifier;
                if (TypeParameters != null && TypeParameters.Any())
                    internalName += "`" + TypeParameters.Count().ToString(CultureInfo.InvariantCulture); 
                if (Parameters != null)
                    internalName += String.Join("_", Parameters.Select(p => p.TypeName));
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
        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public string ReturnType { get; set; }
        public bool HidesBaseImplementation { get; set; }
        public bool IsOverride { get; set; }
        public bool IsStatic { get; set; }
        public bool IsAbstract { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsAsync { get; set; }
        public bool IsSealed { get; set; }
        public MethodDocComment DocComment { get; set; }

        public MethodModel()
        {
            TypeParameters = new List<TypeParameterModel>();
            Parameters = new List<ParameterModel>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ AccessModifier.GetHashCode();
            hashCode = (hashCode * 397) ^ (TypeParameters != null ? TypeParameters.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ HidesBaseImplementation.GetHashCode();
            hashCode = (hashCode * 397) ^ IsOverride.GetHashCode();
            hashCode = (hashCode * 397) ^ IsStatic.GetHashCode();
            hashCode = (hashCode * 397) ^ IsAbstract.GetHashCode();
            hashCode = (hashCode * 397) ^ IsVirtual.GetHashCode();
            hashCode = (hashCode * 397) ^ IsAsync.GetHashCode();
            hashCode = (hashCode * 397) ^ IsSealed.GetHashCode();
            hashCode = (hashCode * 397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);

            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MethodModel);
        }

        public bool Equals(MethodModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                   AccessModifier == other.AccessModifier &&
                   (TypeParameters == null
                       ? (other.TypeParameters == null)
                       : TypeParameters.SequenceEqual(other.TypeParameters)) &&
                   (Parameters == null ? (other.Parameters == null) : Parameters.SequenceEqual(other.Parameters)) &&
                   String.Equals(ReturnType, other.ReturnType) &&
                   HidesBaseImplementation == other.HidesBaseImplementation &&
                   IsOverride == other.IsOverride &&
                   IsStatic == other.IsStatic &&
                   IsAbstract == other.IsAbstract &&
                   IsVirtual == other.IsVirtual &&
                   IsAsync == other.IsAsync &&
                   IsSealed == other.IsSealed &&
                   (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        #endregion
    }
}
