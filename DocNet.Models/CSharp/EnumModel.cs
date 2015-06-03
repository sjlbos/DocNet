using System.Collections.Generic;
using DocNet.Models.Comments;

namespace DocNet.Models.CSharp
{
    public class EnumModel
    {
        public DocComment DocComment { get; set; }
        public IList<string> Fields { get; set; } 
    }
}
