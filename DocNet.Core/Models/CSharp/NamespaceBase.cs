
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Exceptions;

namespace DocNet.Core.Models.CSharp
{
    public abstract class NamespaceBase : CsElement, IParentElement, IEquatable<NamespaceBase>
    {
        private readonly IDictionary<string, INestableElement> _namespaceElements;

        protected NamespaceBase()
        {
            _namespaceElements = new Dictionary<string, INestableElement>();
        }

        #region IParentElement Interface

        public virtual INestableElement this[string uniqueName]
        {
            get { return _namespaceElements.ContainsKey(uniqueName) ? _namespaceElements[uniqueName] : null; }
        }

        public virtual void AddChild(INestableElement child)
        {
            if (child == null)
                throw new ArgumentNullException("child");
            if (String.IsNullOrWhiteSpace(child.UniqueName))
                throw new IllegalChildElementException("Child element has missing name.");
            if (!NestedElementIsLegal(child))
                throw new IllegalChildElementException(GetType() + " cannot have children of type " + child.GetType());
            if (_namespaceElements.ContainsKey(child.UniqueName))
                throw new NamingCollisionException(child.UniqueName);

            child.Parent = this;
            _namespaceElements.Add(child.UniqueName, child);
        }

        public virtual bool HasDirectDescendant(INestableElement child)
        {
            return child != null && _namespaceElements.ContainsKey(child.UniqueName);
        }

        public virtual bool HasDescendant(INestableElement child)
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<INestableElement> GetEnumerator()
        {
            return _namespaceElements.Values.GetEnumerator();
        }

        public bool NestedElementIsLegal(INestableElement element)
        {
            return element is NamespaceModel
                || element is CsType;
        }

        #endregion

        #region Properties

        public IList<NamespaceModel> ChildNamespaces
        {
            get { return _namespaceElements.Values.OfType<NamespaceModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var namespaceModel in value)
                {
                    AddChild(namespaceModel);
                }
            }
        }

        public IList<ClassModel> Classes
        {
            get { return _namespaceElements.Values.OfType<ClassModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var classModel in value)
                {
                    AddChild(classModel);
                }
            }
        }

        public IList<DelegateModel> Delegates
        {
            get { return _namespaceElements.Values.OfType<DelegateModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var delegateModel in value)
                {
                    AddChild(delegateModel);
                }
            }
        }

        public IList<EnumModel> Enums
        {
            get { return _namespaceElements.Values.OfType<EnumModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var enumModel in value)
                {
                    AddChild(enumModel);
                }
            }
        }

        public IList<InterfaceModel> Interfaces
        {
            get { return _namespaceElements.Values.OfType<InterfaceModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var interfaceModel in value)
                {
                    AddChild(interfaceModel);
                }
            }
        }

        public IList<StructModel> Structs
        {
            get { return _namespaceElements.Values.OfType<StructModel>().ToList().AsReadOnly(); }
            set
            {
                if (value == null) return;
                foreach (var structModel in value)
                {
                    AddChild(structModel);
                }
            }
        }

        #endregion

        #region Equality Members

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NamespaceBase);
        }

        public bool Equals(NamespaceBase other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other) &&
                   this.OrderBy(e => e.UniqueName).SequenceEqual(other.OrderBy(e => e.UniqueName));
        }

        #endregion
    }
}
