
using System;

namespace DocNet.Core.Models.CSharp
{
    public abstract class NestableCsElement : CsElement
    {
        public ICsParentElement Parent { get; set; }

        public override string FullName
        {
            get
            {
                if(Parent == null || String.IsNullOrWhiteSpace(Parent.FullName))
                    return Name;
                return Parent.FullName + "." + Name;
            }
        }
    }
}
