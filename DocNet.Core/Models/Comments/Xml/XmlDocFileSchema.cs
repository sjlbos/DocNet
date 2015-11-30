using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DocNet.Core.Models.Comments.Xml
{
    [Serializable]
    [XmlRoot(ElementName = "doc")]
    public class DocFileModel : IEquatable<DocFileModel>
    {
        private Dictionary<string, Member> _nameMemberMap;

        [XmlElement("assembly")]
        public AssemblyModel AssemblyModel { get; set; }

        private List<Member> _members;
        [XmlElement(ElementName = "members")]
        public List<Member> Members {
            get { return _members; }
            set
            {
                _members = value;
                GenerateMemberNameMapping();
            } 
        }

        public DocFileModel()
        {
            Members = new List<Member>();
        }

        private void GenerateMemberNameMapping()
        {
            if (_members == null)
            {
                _nameMemberMap = null;
                return;
            }
            _nameMemberMap = new Dictionary<string, Member>();
            foreach (Member m in _members)
            {
                // Todo
            }
        }

        public Member GetMemberByFullName(string fullName)
        {
            if (_nameMemberMap == null || !_nameMemberMap.ContainsKey(fullName))
                return null;

            return _nameMemberMap[fullName];
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = AssemblyModel != null ? AssemblyModel.GetHashCode() : 0;
                return (hashCode * 397) ^ (Members != null ? Members.GetHashCode() : 0);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DocFileModel);
        }

        public bool Equals(DocFileModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return AssemblyModel == null ? (other.AssemblyModel == null) : AssemblyModel.Equals(other.AssemblyModel)
                && Members == null ? (other.Members == null) : Members.SequenceEqual(other.Members);
        }

        #endregion
    }

    [Serializable]
    [XmlType(TypeName = "assembly")]
    public class AssemblyModel : IEquatable<AssemblyModel>
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
            return Equals(obj as AssemblyModel);
        }

        public bool Equals(AssemblyModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name);
        }

        #endregion
    }

    [Serializable]
    [XmlType(TypeName = "member")]
    public class Member : IEquatable<Member>
    {
        private static readonly Regex MemberNameRegex = new Regex(@"\A(N|T|F|P|M|E|!):([^\s]+)\Z");

        public DocFileMemberType MemberType { get; set; }

        private string _fullMemberName;
        [XmlAttribute("name")]
        public string FullMemberName
        {
            get { return _fullMemberName; }
            set
            {
                if (!MemberNameIsValid(value))
                    throw new SerializationException("");
                _fullMemberName = value;
                MemberType = GetMembertypeFromChar(_fullMemberName[0]);
            }
        }

        [XmlText(typeof(string))]
        public List<object> Items;  

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
                hashCode = (hashCode*397) ^ MemberType.GetHashCode(); 
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Member);
        }

        public bool Equals(Member other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(FullMemberName, other.FullMemberName)
                   && MemberType == other.MemberType
                   && (Items == null) ? (other.Items == null) : Items.SequenceEqual(other.Items);
        }

        #endregion

        #region Helper Methods

        private static bool MemberNameIsValid(string name)
        {
            return name != null && MemberNameRegex.Match(name).Success;
        }

        private DocFileMemberType GetMembertypeFromChar(char firstCharacter)
        {
            switch (firstCharacter)
            {
                case 'M':
                    return DocFileMemberType.Method;
                case 'T':
                    return DocFileMemberType.Type;
                case 'P':
                    return DocFileMemberType.Property;
                case 'N':
                    return DocFileMemberType.Namespace;
                case 'F':
                    return DocFileMemberType.Field;
                case 'E':
                    return DocFileMemberType.Event;
            }
            throw new ArgumentException("Invalid first character for member element name: " + firstCharacter);
        }

        #endregion
    }

    public enum DocFileMemberType
    {
        Field,
        Property,
        Method,
        Type,
        Event,
        Namespace
    }
}