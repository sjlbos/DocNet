using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Exceptions;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public abstract class InterfaceBase : CsType, ICsParentElement, IEquatable<InterfaceBase>
    {
        #region Element Management

        private readonly IDictionary<string, NestableCsElement> _interfaceElements;

        public void AddChild(NestableCsElement child)
        {
            if (child == null)
                throw new ArgumentNullException("child");
            if (String.IsNullOrWhiteSpace(child.Name) || String.IsNullOrWhiteSpace(child.FullName))
                throw new IllegalChildElementException("Child element has missing name.");
            if (!NestedElementIsLegal(child))
                throw new IllegalChildElementException("Interface can not have children of type " + child.GetType());

            if (child.Parent == null || NestedElementIsDirectDescendant(child))
            {
                child.Parent = this;

                if (_interfaceElements.ContainsKey(child.Name))
                    throw new NamingCollisionException(child.Name);

                _interfaceElements.Add(child.Name, child);
            }

            if (Parent != null)
                Parent.AddChild(child);
        }

        public NestableCsElement this[string name]
        {
            get
            {
                if (_interfaceElements.ContainsKey(name))
                    return _interfaceElements[name];
                if (Parent != null)
                    return Parent[name];
                return null;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<NestableCsElement> GetEnumerator()
        {
            return _interfaceElements.Values.GetEnumerator();
        }

        #endregion

        #region Properties

        public IList<TypeParameterModel> TypeParameters { get; set; }
        public IList<string> InheritanceList { get; set; }
        public InterfaceDocComment DocComment { get; set; }

        public IList<MethodModel> Methods
        {
            get { return this.OfType<MethodModel>().ToList().AsReadOnly(); }
            set
            {
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
                foreach (var property in value)
                {
                    AddChild(property);
                }
            }
        }

        #endregion

        protected InterfaceBase()
        {
            _interfaceElements = new Dictionary<string, NestableCsElement>();
            TypeParameters = new List<TypeParameterModel>();
            InheritanceList = new List<string>();
        }

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
                    (TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)) &&
                    (Properties == null ? (other.Properties == null) : Properties.SequenceEqual(other.Properties)) &&
                    (Methods == null ? (other.Methods == null) : Methods.SequenceEqual(other.Methods)) &&
                    (InheritanceList == null ? (other.InheritanceList == null) : InheritanceList.SequenceEqual(other.InheritanceList)) &&
                    (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        #endregion

        protected bool NestedElementIsDirectDescendant(NestableCsElement element)
        {
            return element.Parent == this;
        }

        protected virtual bool NestedElementIsLegal(NestableCsElement element)
        {
            return element is MethodModel ||
                   element is PropertyModel;
        }
    }
}
