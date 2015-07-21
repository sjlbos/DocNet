using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DocNet.Core.Exceptions;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public abstract class InterfaceBase : CsType, IParentElement, IEquatable<InterfaceBase>
    {
        protected InterfaceBase()
        {
            _interfaceElements = new Dictionary<string, INestableElement>();
            TypeParameters = new List<TypeParameterModel>();
            InheritanceList = new List<string>();
        }

        #region IParentElement Interface

        public virtual bool NestedElementIsLegal(INestableElement element)
        {
            return element is MethodModel
                || element is PropertyModel;
        }

        private readonly IDictionary<string, INestableElement> _interfaceElements;

        public void AddChild(INestableElement child)
        {
            if (child == null)
                throw new ArgumentNullException("child");
            if (String.IsNullOrWhiteSpace(child.InternalName))
                throw new IllegalChildElementException("Child element has missing name.");
            if (!NestedElementIsLegal(child))
                throw new IllegalChildElementException(GetType() + " cannot have children of type " + child.GetType());
            if (_interfaceElements.ContainsKey(child.InternalName))
                throw new NamingCollisionException(child.InternalName);

            child.Parent = this;
            _interfaceElements.Add(child.InternalName, child);
        }

        public bool HasDirectDescendant(INestableElement child)
        {
            return child != null && _interfaceElements.ContainsKey(child.InternalName);
        }

        public bool HasDescendant(INestableElement child)
        {
            if (child == null)
                return false;

            while (child.Parent != null)
            {
                if (child.Parent == this)
                    return true;

                child = child.Parent as INestableElement;

                if (child == null)
                    return false;
            }
            return false;
        }

        public virtual INestableElement this[string internalName]
        {
            get { return _interfaceElements.ContainsKey(internalName) ? _interfaceElements[internalName] : null; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<INestableElement> GetEnumerator()
        {
            return _interfaceElements.Values.GetEnumerator();
        }

        #endregion

        #region Properties

        public override string DisplayName
        {
            get
            {
                if (Identifier == null) return null;
                if (!TypeParameters.Any()) return Identifier;
                return Identifier + "<" + String.Join(",", TypeParameters.Select(p => p.Name)) + ">";
            }
        }

        public override string InternalName
        {
            get
            {
                if(Identifier == null) return null;
                return Identifier + 
                ((TypeParameters != null && TypeParameters.Any()) 
                    ? "`" + TypeParameters.Count().ToString(CultureInfo.InvariantCulture) 
                    : String.Empty); 
            }
        }

        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<string> InheritanceList { get; set; }
        public InterfaceDocComment DocComment { get; set; }
        public bool IsPartial { get; set; }

        public IList<MethodModel> Methods
        {
            get { return this.OfType<MethodModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var method in value)
                {
                    AddChild(method);
                }
            }
        }

        public IList<PropertyModel> Properties
        {
            get { return this.OfType<PropertyModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var property in value)
                {
                    AddChild(property);
                }
            }
        }

        #endregion

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (DocComment != null ? DocComment.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InterfaceBase);
        }

        public bool Equals(InterfaceBase other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other) &&
                    IsPartial == other.IsPartial &&
                    (TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)) &&
                    (Properties == null ? (other.Properties == null) : Properties.SequenceEqual(other.Properties)) &&
                    (Methods == null ? (other.Methods == null) : Methods.SequenceEqual(other.Methods)) &&
                    (InheritanceList == null ? (other.InheritanceList == null) : InheritanceList.SequenceEqual(other.InheritanceList)) &&
                    (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        #endregion

    }
}
