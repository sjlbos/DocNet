
using System;

namespace DocNet.Core.Models.CSharp
{
    public class GlobalNamespaceModel : NamespaceBase
    {
        #region Properties

        public override string Name
        {
            get { return string.Empty; }
            set { /* do nothing */ }
        }

        public override string UniqueName
        {
            get { return "::global"; }
        }

        public override string FullName
        {
            get { return String.Empty; }
        }

        #endregion
    }
}
