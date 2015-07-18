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

            var namespaceElement = child as NamespaceElement;
            if (namespaceElement != null)
            {
                if (child.Parent == null || NamespaceElementIsDirectDescendant(child))
                {
                    child.Parent = this;
                    if (_directDescendants.ContainsKey(child.UniqueName)) 
                        throw new NamingCollisionException(child.UniqueName);
                    _directDescendants.Add(child.UniqueName, namespaceElement);
                }
            }

            // Only the root (global) namespace will index by full name
            if (Parent == null)
            {
                if (_namespaceElements.ContainsKey(child.FullName))
                    throw new NamingCollisionException(child.FullName);

                _namespaceElements.Add(child.FullName, child);
                var parentElement = child as ICsParentElement;
                if (parentElement != null)
                {
                    foreach (var childElement in (parentElement))
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

        public NestableCsElement this[string uniqueName]
        {
            get
            {       
                if (_directDescendants.ContainsKey(uniqueName)) 
                    return _directDescendants[uniqueName]; 
                if (Parent != null) 
                    return Parent[uniqueName];
                if (_namespaceElements.ContainsKey(uniqueName)) 
                    return _namespaceElements[uniqueName]; 
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

        public override string UniqueName
        {
            get { return Name; }
        }

        public IList<NamespaceModel> ChildNamespaces
        {
            get { return _directDescendants.Values.OfType<NamespaceModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
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
                if(value == null) return;
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
                if(value == null) return;
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
                if(value == null) return;
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
                if(value == null) return;
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
                if(value == null) return;
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
