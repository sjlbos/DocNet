using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class DelegateModel : CsType, IEquatable<DelegateModel>
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

        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<ParameterModel> Parameters { get; set; }
        public string ReturnType { get; set; }
        public MethodDocComment DocComment { get; set; }

        public DelegateModel()
        {
            TypeParameters = new List<TypeParameterModel>();
            Parameters = new List<ParameterModel>();
        }

        #region Equality Members 

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DelegateModel);
        }

        public bool Equals(DelegateModel other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return base.Equals(other) &&
                String.Equals(ReturnType, other.ReturnType) &&
                (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment)) &&
                (TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)) &&
                (Parameters == null ? (other.Parameters == null) : Parameters.SequenceEqual(other.Parameters));   
        }

        #endregion
    }
}
