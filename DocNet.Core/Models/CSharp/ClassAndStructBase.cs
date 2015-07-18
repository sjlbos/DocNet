using System.Collections.Generic;
using System.Linq;

namespace DocNet.Core.Models.CSharp
{
    public abstract class ClassAndStructBase : InterfaceBase
    {
        #region Properties

        public IList<ConstructorModel> Constructors
        {
            get { return this.OfType<ConstructorModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var constructor in value)
                {
                    AddChild(constructor);
                }
            }
        }

        public IList<InterfaceModel> InnerInterfaces
        {
            get { return this.OfType<InterfaceModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var childInterface in value)
                {
                    AddChild(childInterface);
                }
            }
        } 

        public IList<ClassModel> InnerClasses
        {
            get { return this.OfType<ClassModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var childClass in value)
                {
                    AddChild(childClass);
                }
            }
        }

        public IList<StructModel> InnerStructs
        {
            get { return this.OfType<StructModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var childStruct in value)
                {
                    AddChild(childStruct);
                }
            }
        }

        public IList<EnumModel> InnerEnums
        {
            get { return this.OfType<EnumModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var childEnum in value)
                {
                    AddChild(childEnum);
                }
            }
        }

        public IList<DelegateModel> InnerDelegates
        {
            get { return this.OfType<DelegateModel>().ToList().AsReadOnly(); }
            set
            {
                if(value == null) return;
                foreach (var childDelegate in value)
                {
                    AddChild(childDelegate);
                }
            }
        }

        #endregion

        protected override bool NestedElementIsLegal(NestableCsElement element)
        {
            return base.NestedElementIsLegal(element) ||
                   element is ConstructorModel ||
                   element is CsType;
        }
    }
}