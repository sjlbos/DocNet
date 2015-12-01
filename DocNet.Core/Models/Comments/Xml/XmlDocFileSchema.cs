using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace DocNet.Core.Models.Comments.Xml
{
    [Serializable]
    [XmlRoot(ElementName = "doc")]
    public class DocFileModel : IEquatable<DocFileModel>
    {
        [XmlElement("assembly")]
        public AssemblyModel AssemblyModel { get; set; }

        [XmlElement("members")]
        public MemberList MemberList { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = AssemblyModel != null ? AssemblyModel.GetHashCode() : 0;
                return (hashCode * 397) ^ (MemberList != null ? MemberList.GetHashCode() : 0);
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
                && MemberList == null ? (other.MemberList == null) : MemberList.Equals(other.MemberList);
        }

        #endregion
    }

    [Serializable]
    [XmlType("assembly")]
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
    [XmlType("members")]
    public class MemberList : IEquatable<MemberList>
    {
        private Dictionary<string, Member> _nameMemberMap;

        private Member[] _members;
        [XmlElement("member", typeof(Member))]
        public Member[] Members {
            get { return _members; }
            set
            {
                _members = value;
                GenerateMemberNameMapping();
            }
        }

        private void GenerateMemberNameMapping()
        {
            if (Members == null)
            {
                _nameMemberMap = null;
                return;
            }
            _nameMemberMap = new Dictionary<string, Member>();
            foreach (var m in Members)
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
            int hashCode = 0;
            if (Members == null) return hashCode;
            foreach (var m in Members)
            {
                hashCode = (hashCode*397) ^ m.GetHashCode();
            }
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MemberList);
        }

        public bool Equals(MemberList other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Members == null ? (other.Members == null) : Members.SequenceEqual(other.Members);
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

        // We want the doc comment XML of each member to be stored as a string instead of being deserialized directly into a DocComment object.
        // This is because which DocComment type the XML should get deserialized to depends on the member type.
        // Therefore, we store all commment nodes as-is and then recombine them as a string when they are requested through the "DocCommentXml" property.
        [XmlAnyElement]
        public XmlElement[] CommentNodes;

        [XmlIgnore]
        public string DocCommentXml
        {
            get
            {
                if (CommentNodes == null) return null;
                StringBuilder sb = new StringBuilder();
                foreach (var node in CommentNodes)
                {
                    sb.Append(node.OuterXml);
                }
                return sb.ToString();
            }
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = FullMemberName != null ? FullMemberName.GetHashCode() : 0;
                hashCode = (hashCode*397) ^ MemberType.GetHashCode();
                hashCode = (hashCode * 397) ^ DocCommentXml.GetHashCode(); 
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
                   && String.Equals(DocCommentXml, other.DocCommentXml);
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