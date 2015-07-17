using System.Collections.Generic;
using DocNet.Core.Models.Comments;

namespace DocNet.Core.Models.CSharp
{
    public class EnumModel : CsType
    {
        public override string UniqueName
        {
            get { return Name; }
        }

        public DocComment DocComment { get; set; }
        public IList<string> Fields { get; set; } 
    }
}
