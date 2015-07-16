using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DocNet.Core.Exceptions;

namespace DocNet.Core.Models.CSharp
{
    public class NamespaceModel : NamespaceElement, ICsParentElement, IEquatable<NamespaceModel>
    {
        private readonly IDictionary<string, NestableCsElement> _namespaceElements;
        private readonly IDictionary<string, NamespaceElement> _directDescendants;

        #region Element Management

        public void AddChild(NestableCsElement child)
        {
            if (child == null) 
                throw new ArgumentNullException("child");
            if (String.IsNullOrWhiteSpace(child.Name) || String.IsNullOrWhiteSpace(child.FullName))
                throw new IllegalChildElementException("Child element has missing name.");

            if (child is NamespaceElement)
            {
                if (child.Parent == null || NamespaceElementIsDirectDescendant(child))
                {
                    child.Parent = this;
                    if (_directDescendants.ContainsKey(child.Name)) throw new NamingCollisionException(child.Name);
                    _directDescendants.Add(child.Name, child as NamespaceElement);
                }
            }

            // Only the root (global) namespace will index by full name
            if (Parent == null)
            {
                if (_namespaceElements.ContainsKey(child.FullName))
                    throw new NamingCollisionException(child.FullName);

                _namespaceElements.Add(child.FullName, child);
                if (child is ICsParentElement)
                {
                    foreach (var childElement in (child as ICsParentElement))
                    {
                        AddChild(childElement);
                    }
                }
            }
            else
            {
                Parent.AddChild(child);
            }
        }

        public NestableCsElement this[string name]
        {
            get
            {       
                if (_directDescendants.ContainsKey(name)) 
                    return _directDescendants[name]; 
                if (Parent != null) 
                    return Parent[name];
                if (_namespaceElements.ContainsKey(name)) 
                    return _namespaceElements[name]; 
                return null;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<NestableCsElement> GetEnumerator()
        {
            return _namespaceElements.Values.GetEnumerator();
        }

        #endregion

        #region Properties

        public IList<NamespaceModel> ChildNamespaces
        {
            get { return _directDescendants.Values.OfType<NamespaceModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var namespaceModel in value)
                {
                    AddChild(namespaceModel);
                }
            }
        }

        public IList<ClassModel> Classes
        {
            get { return _directDescendants.Values.OfType<ClassModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var classModel in value)
                {
                    AddChild(classModel);
                }
            }
        }

        public IList<DelegateModel> Delegates
        {
            get { return _directDescendants.Values.OfType<DelegateModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var delegateModel in value)
                {
                    AddChild(delegateModel);
                }
            }
        }

        public IList<EnumModel> Enums
        {
            get { return _directDescendants.Values.OfType<EnumModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var enumModel in value)
                {
                    AddChild(enumModel);
                }
            }
        }

        public IList<InterfaceModel> Interfaces
        {
            get { return _directDescendants.Values.OfType<InterfaceModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var interfaceModel in value)
                {
                    AddChild(interfaceModel);
                }
            }
        }

        public IList<StructModel> Structs
        {
            get { return _directDescendants.Values.OfType<StructModel>().ToList().AsReadOnly(); }
            set
            {
                foreach (var structModel in value)
                {
                    AddChild(structModel);
                }
            }
        }

        #endregion

        public NamespaceModel()
        {
            _namespaceElements = new Dictionary<string, NestableCsElement>();
            _directDescendants = new Dictionary<string, NamespaceElement>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NamespaceModel);
        }

        public bool Equals(NamespaceModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other) &&
                   this.OrderBy(e => e.FullName).SequenceEqual(other.OrderBy(e => e.FullName));
        }

        #endregion

        private bool NamespaceElementIsDirectDescendant(NestableCsElement element)
        {
            return element.Parent == this;
        }
    }
}
