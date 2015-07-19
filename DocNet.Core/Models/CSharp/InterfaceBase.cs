using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            if (String.IsNullOrWhiteSpace(child.Name))
                throw new IllegalChildElementException("Child element has missing name.");
            if (!NestedElementIsLegal(child))
                throw new IllegalChildElementException(GetType() + " cannot have children of type " + child.GetType());

            if (child.Parent == null || NestedElementIsDirectDescendant(child))
            {
                child.Parent = this;

                if (_interfaceElements.ContainsKey(child.UniqueName))
                {
                    if (_interfaceElements[child.UniqueName] != child)
                    {
                        throw new NamingCollisionException(child.UniqueName);
                    }
                }

                _interfaceElements.Add(child.UniqueName, child);
            }

            if (Parent != null)
                Parent.AddChild(child);
        }

        public NestableCsElement this[string uniqueName]
        {
            get
            {
                if (_interfaceElements.ContainsKey(uniqueName))
                    return _interfaceElements[uniqueName];
                if (Parent != null)
                    return Parent[uniqueName];
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

        public override string UniqueName
        {
            get
            {
                if(Name == null) return null;
                return Name + 
                ((TypeParameters != null && TypeParameters.Any()) 
                    ?  TypeParameters.Count().ToString(CultureInfo.InvariantCulture) 
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
                    IsPartial == other.IsPartial &&
                    (TypeParameters == null ? (other.TypeParameters == null) : TypeParameters.SequenceEqual(other.TypeParameters)) &&
                    (Properties == null ? (other.Properties == null) : Properties.SequenceEqual(other.Properties)) &&
                    (Methods == null ? (other.Methods == null) : Methods.SequenceEqual(other.Methods)) &&
                    (InheritanceList == null ? (other.InheritanceList == null) : InheritanceList.SequenceEqual(other.InheritanceList)) &&
                    (DocComment == null ? (other.DocComment == null) : DocComment.Equals(other.DocComment));
        }

        #endregion

        protected bool NestedElementIsDirectDescendant(NestableCsElement element)
        {
            if(element == null) return false;
            return element.Parent == this;
        }

        protected virtual bool NestedElementIsLegal(NestableCsElement element)
        {
            return element is MethodModel ||
                   element is PropertyModel;
        }
    }
}
