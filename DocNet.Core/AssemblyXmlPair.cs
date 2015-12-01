using System;

namespace DocNet.Core
{
    public class AssemblyXmlPair : IEquatable<AssemblyXmlPair>
    {

        public string AssemblyFilePath { get; set; }
        public string XmlFilePath { get; set; }

        public override int GetHashCode()
        {
            int hashCode = AssemblyFilePath != null ? AssemblyFilePath.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (XmlFilePath != null ? XmlFilePath.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AssemblyXmlPair);
        }

        public bool Equals(AssemblyXmlPair other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(AssemblyFilePath, other.AssemblyFilePath) &&
                String.Equals(XmlFilePath, other.XmlFilePath);
        }

    }
}
