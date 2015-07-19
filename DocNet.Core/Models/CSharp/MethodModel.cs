using System;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class MethodModel : NestableCsElement, IEquatable<MethodModel>
    {
        public override string UniqueName
        {
            get
            {
                var outputString = Name;
                if(Parameters != null && Parameters.Any())
                {
                    outputString += "_";
                    outputString += String.Join("_", Parameters.Select(p => p.TypeName));
                }
                return outputString;
            }
        }

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
