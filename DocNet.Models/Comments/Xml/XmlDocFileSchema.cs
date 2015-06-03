using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using DocNet.Models.CSharp;

namespace DocNet.Models.Comments.Xml
{
    [Serializable]
    [XmlRoot(ElementName = "doc")]
    public class Documentation : IEquatable<Documentation>
    {
        [XmlElement("assembly")]
        public Assembly Assembly { get; set; }

        [XmlElement(ElementName = "members")]
        public IList<Member> Members { get; set; }

        public Documentation()
        {
            Members = new List<Member>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Assembly != null ? Assembly.GetHashCode() : 0;
                return (hashCode * 397) ^ (Members != null ? Members.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Documentation);
        }

        public bool Equals(Documentation other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return Assembly == null ? (other.Assembly == null) : Assembly.Equals(other.Assembly)
                && Members == null ? (other.Members == null) : Members.SequenceEqual(other.Members);
        }

        #endregion
    }

    [Serializable]
    [XmlType(TypeName = "assembly")]
    public class Assembly : IEquatable<Assembly>
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Assembly);
        }

        public bool Equals(Assembly other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return String.Equals(Name, other.Name);
        }

        #endregion
    }

    [Serializable]
    [XmlType(TypeName = "member")]
    public class Member : IEquatable<Member>
    {
        private static readonly Regex MemberNameRegex = new Regex(@"\A(N|T|F|P|M|E|!):([^\s]+)\Z");

        private string _fullMemberName;
        [XmlAttribute("name")]
        public string FullMemberName
        {
            get { return _fullMemberName; }
            set
            {
                _fullMemberName = value;
                SetMemberPropertiesFromName(_fullMemberName);
            }
        }

        [XmlElement("example", typeof(ExampleTag))]
        [XmlElement("exception", typeof(ExceptionTag))]
        [XmlElement("include", typeof(IncludeTag))]
        [XmlElement("param", typeof(ParameterTag))]
        [XmlElement("permission", typeof(PermissionTag))]
        [XmlElement("remarks", typeof(RemarksTag))]
        [XmlElement("returns", typeof(ReturnsTag))]
        [XmlElement("seealso", typeof(SeeAlsoTag))]
        [XmlElement("summary", typeof(SummaryTag))]
        [XmlElement("typeparam", typeof(TypeParameterTag))]
        [XmlElement("value", typeof(ValueTag))]
        public IList<object> Items { get; set; } 

        public CsElement CsElement { get; set; }

        public Member()
        {
            Items = new List<object>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = FullMemberName != null ? FullMemberName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ CsElement.GetHashCode();
                hashCode = (hashCode * 397) ^ (Items != null ? Items.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Member);
        }

        public bool Equals(Member other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return String.Equals(FullMemberName, other.FullMemberName)
                   && CsElement == other.CsElement
                   && (Items == null)
                ? (other.Items == null)
                : Items.SequenceEqual(other.Items);
        }

        #endregion

        #region Helper Methods

        private void SetMemberPropertiesFromName(string name)
        {
            if(!MemberNameIsValid(name))
                throw new SerializationException("");

            CsElement = GetCsElementFromChar(name[0]);
        }

        private static bool MemberNameIsValid(string name)
        {
            return name != null && MemberNameRegex.Match(name).Success;
        }

        private CsElement GetCsElementFromChar(char firstCharacter)
        {
            switch (firstCharacter)
            {
                case 'M':
                    return CsElement.Method;
                case 'T':
                    return CsElement.Type;
                case 'P':
                    return CsElement.Property;
                case 'N':
                    return CsElement.Namespace;
                case 'F':
                    return CsElement.Field;
                case 'E':
                    return CsElement.Event;
            }
            throw new ArgumentException("Invalid first character for member element name: " + firstCharacter);
        }

        #endregion
    }
}
