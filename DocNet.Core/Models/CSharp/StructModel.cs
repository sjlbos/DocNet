
using System;

namespace DocNet.Core.Models.CSharp
{
    public class StructModel : ClassAndStructBase, IEquatable<StructModel>
    {
        public bool Equals(StructModel other)
        {
            return base.Equals(other);
        }
    }
}
