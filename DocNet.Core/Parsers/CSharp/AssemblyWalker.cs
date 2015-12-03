using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DocNet.Core.Parsers.CSharp
{
    public abstract class AssemblyWalker
    {
        private readonly Assembly _assembly;

        protected AssemblyWalker(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            _assembly = assembly;
        }

        public virtual void VisitAssembly()
        {
            var namespaceList = _assembly.GetTypes().Select(t => t.Namespace).Distinct().ToList();
            foreach (var namespaceName in namespaceList)
            {
                VisitNamespace(namespaceName);
            }
        }

        public virtual void VisitNamespace(string namespaceName)
        {
            IList<Type> namespaceElements = namespaceName == null ? 
                _assembly.GetTypes().Where(t => t.Namespace == null).ToList()
                : _assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Equals(namespaceName)).ToList();
            foreach (Type t in namespaceElements)
            {
                VisitType(t);
            }
        }

        public virtual void VisitType(Type t)
        {
            if (IsDelegate(t))
            {
                VisitDelegate(t);
            }
            else if (t.IsClass)
            {
                VisitClass(t);
            }
            else if (t.IsInterface)
            {
                VisitInterface(t);
            }
            else if (t.IsValueType)
            {
                if (t.IsEnum)
                {
                    VisitEnum(t);
                }
                else
                {
                    VisitStruct(t);
                }
            }
        }

        public virtual void VisitClass(Type t)
        {
            IterateMembersOfType(t);
        }

        public virtual void VisitInterface(Type t)
        {
            IterateMembersOfType(t);   
        }

        public virtual void VisitStruct(Type t)
        {
            IterateMembersOfType(t);
        }

        public virtual void VisitEnum(Type t)
        {

        }

        public virtual void VisitDelegate(Type t)
        {
            
        }

        public virtual void VisitConstructor(ConstructorInfo constructorInfo)
        {
            
        }

        public virtual void VisitMethod(MethodInfo methodInfo)
        {
            
        }

        public virtual void VisitProperty(PropertyInfo propertyInfo)
        {
            
        }

        public virtual void VisitField(FieldInfo fieldInfo)
        {
            
        }

        public virtual void VisitEvent(EventInfo eventInfo)
        {
            
        }

        #region Helper Methods

        private bool IsDelegate(Type t)
        {
            return typeof(MulticastDelegate).IsAssignableFrom(t.BaseType);
        }

        private void IterateMembersOfType(Type t)
        {
            foreach (var member in t.GetMembers())
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Method:
                        VisitMethod(member as MethodInfo);
                        break;
                    case MemberTypes.Property:
                        VisitProperty(member as PropertyInfo);
                        break;
                    case MemberTypes.Constructor:
                        VisitConstructor(member as ConstructorInfo);
                        break;
                    case MemberTypes.Field:
                        VisitField(member as FieldInfo);
                        break;
                    case MemberTypes.Event:
                        VisitEvent(member as EventInfo);
                        break;
                    case MemberTypes.NestedType:
                    case MemberTypes.TypeInfo:
                        VisitType(((TypeInfo)member).AsType());
                        break;
                }
            }
        }

        #endregion 
    }
}
