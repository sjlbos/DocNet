
using System;
using System.Collections.Generic;

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
            get { return String.Empty; }
        }

        public override string FullName
        {
            get { return String.Empty; }
        }

        #endregion
    }
}
