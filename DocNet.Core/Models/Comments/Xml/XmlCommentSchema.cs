using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace DocNet.Core.Models.Comments.Xml
{
    public abstract class Container : IEquatable<Container>
    {   
        [XmlText(typeof(string))]
        [XmlElement("c", typeof(CTag))]
        [XmlElement("code", typeof(CodeTag))]
        [XmlElement("list", typeof(ListTag))]
        [XmlElement("paramref", typeof(ParameterReferenceTag))]
        [XmlElement("see", typeof(SeeTag))]
        [XmlElement("typeparamref", typeof(TypeParameterReferenceTag))]
        public List<object> Items { get; set; }

        protected Container()
        {
            Items = new List<object>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return Items != null ? Items.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Container);
        }

        public bool Equals(Container other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return Items == null ? (other.Items == null) : Items.SequenceEqual(other.Items);
        }

        #endregion
    }

    public abstract class ReferenceElement : IEquatable<ReferenceElement>
    {
        [XmlAttribute("cref")]
        public string ReferencedElementName { get; set; }

        [XmlText]
        public List<string> Text { get; set; }

        protected ReferenceElement()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = ReferencedElementName != null ? ReferencedElementName.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ReferenceElement);
        }

        public bool Equals(ReferenceElement other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(ReferencedElementName, other.ReferencedElementName)
                   && Text == null
                ? (other.Text == null)
                : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    #region Top Level Elements

    public abstract class TopLevelContainer : IEquatable<TopLevelContainer>
    {
        [XmlText(typeof(string))]
        [XmlElement("c", typeof(CTag))]
        [XmlElement("code", typeof(CodeTag))]
        [XmlElement("list", typeof(ListTag))]
        [XmlElement("paramref", typeof(ParameterReferenceTag))]
        [XmlElement("see", typeof(SeeTag))]
        [XmlElement("typeparamref", typeof(TypeParameterReferenceTag))]
        [XmlElement("para", typeof(ParagraphTag))]
        public List<object> Items { get; set; }

        
        protected TopLevelContainer()
        {
            Items = new List<object>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return Items != null ? Items.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TopLevelContainer);
        }

        public bool Equals(TopLevelContainer other)
        {
            if(other == null) return false;
            if(this == other) return true;
            return Items == null ? (other.Items == null) : Items.SequenceEqual(other.Items);
        }

        #endregion
    }

    [Serializable]
    [XmlType("summary")]
    public class SummaryTag : TopLevelContainer { }

    [Serializable]
    [XmlType("remarks")]
    public class RemarksTag : TopLevelContainer { }

    [Serializable]
    [XmlType("example")]
    public class ExampleTag : TopLevelContainer { }

    [Serializable]
    [XmlType("returns")]
    public class ReturnsTag : TopLevelContainer { }

    [Serializable]
    [XmlType("value")]
    public class ValueTag : TopLevelContainer { }

    [Serializable]
    [XmlType("param")]
    public class ParameterTag : TopLevelContainer, IEquatable<ParameterTag>
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ParameterTag);
        }

        public bool Equals(ParameterTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                   && String.Equals(Name, other.Name);
        }

        #endregion
    }

    [Serializable]
    [XmlType("typeparam")]
    public class TypeParameterTag : TopLevelContainer, IEquatable<TypeParameterTag>
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeParameterTag);
        }

        public bool Equals(TypeParameterTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                   && String.Equals(Name, other.Name);
        }

        #endregion
    }

    [Serializable]
    [XmlType("exception")]
    public class ExceptionTag : TopLevelContainer, IEquatable<ExceptionTag>
    {
        [XmlAttribute("cref")]
        public string ExceptionType { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (ExceptionType != null ? ExceptionType.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExceptionTag);
        }

        public bool Equals(ExceptionTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return base.Equals(other)
                   && String.Equals(ExceptionType, other.ExceptionType);
        }

        #endregion
    }

    [Serializable]
    [XmlType("seealso")]
    public class SeeAlsoTag : ReferenceElement { }

    [Serializable]
    [XmlType("permission")]
    public class PermissionTag : ReferenceElement { }

    [Serializable]
    [XmlType("include")]
    public class IncludeTag
    {
        [XmlAttribute("file")]
        public string FileName { get; set; }

        [XmlAttribute("path")]
        public string FilePath { get; set; }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = FileName != null ? FileName.GetHashCode() : 0;
                hashCode = (hashCode *397) ^ (FilePath != null ? FilePath.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IncludeTag);
        }

        public bool Equals(IncludeTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(FileName, other.FileName)
                   && String.Equals(FilePath, other.FilePath);
        }

        #endregion
    }

    #endregion

    #region Child Elements

    [Serializable]
    [XmlType("para")]
    public class ParagraphTag : Container { }

    [Serializable]
    [XmlType("see")]
    public class SeeTag : ReferenceElement { }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix"), Serializable]
    [XmlType("c")]
    public class CTag : IEquatable<CTag>
    {
        [XmlText]
        public List<string> Text { get; set; }

        public CTag()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CTag);
        }

        public bool Equals(CTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Text == null ? (other.Text == null) : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    [Serializable]
    [XmlType("code")]
    public class CodeTag : IEquatable<CodeTag>
    {
        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlText]
        public List<string> Text { get; set; }

        public CodeTag()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Language != null ? Language.GetHashCode() : 0;
                hashCode = (hashCode*397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CodeTag);
        }

        public bool Equals(CodeTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Language, other.Language)
                   && Text == null
                ? (other.Text == null)
                : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    [Serializable]
    [XmlType("paramref")]
    public class ParameterReferenceTag : IEquatable<ParameterReferenceTag>
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public List<string> Text { get; set; }

        public ParameterReferenceTag()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ParameterReferenceTag);
        }

        public bool Equals(ParameterReferenceTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name)
                   && Text == null
                ? (other.Text == null)
                : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    [Serializable]
    [XmlType("typeparamref")]
    public class TypeParameterReferenceTag : IEquatable<TypeParameterReferenceTag>
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public List<string> Text { get; set; }

        public TypeParameterReferenceTag()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Name != null ? Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeParameterReferenceTag);
        }

        public bool Equals(TypeParameterReferenceTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name)
                   && Text == null
                ? (other.Text == null)
                : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    #region List

    public enum ListType
    {
        [XmlEnum("bullet")]
        Bullet,
        [XmlEnum("number")]
        Number,
        [XmlEnum("table")]
        Table
    }

    [Serializable]
    [XmlType("list")]
    public class ListTag : IEquatable<ListTag>
    {
        [XmlAttribute("type")]
        public ListType ListType { get; set; }

        public ListHeader Header { get; set; }

        public List<ListItem> Elements { get; set; }

        public ListTag()
        {
            Elements = new List<ListItem>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = ListType.GetHashCode();
                hashCode = (hashCode*397) ^ (Header != null ? Header.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Elements != null ? Elements.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ListTag);
        }

        public bool Equals(ListTag other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return ListType == other.ListType
                   && Header == null
                ? (other.Header == null)
                : Header.Equals(other.Header)
                  && Elements == null
                    ? (other.Elements == null)
                    : Elements.SequenceEqual(other.Elements);
        }

        #endregion
    }

    public abstract class ListElement : IEquatable<ListElement>
    {
        public List<Term> Terms { get; set; }
        public List<Description> Descriptions { get; set; }

        protected ListElement()
        {
            Terms = new List<Term>();
            Descriptions = new List<Description>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ListElement);
        }

        public bool Equals(ListElement other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Terms == null ? (other.Terms == null) : Terms.SequenceEqual(other.Terms)
                && Descriptions == null ? (other.Descriptions == null) : Descriptions.SequenceEqual(other.Descriptions);
        }

        #endregion
    }

    [Serializable]
    [XmlType("listheader")]
    public class ListHeader : ListElement { }

    [Serializable]
    [XmlType("item")]
    public class ListItem : ListElement { }

    [Serializable]
    [XmlType("term")]
    public class Term : IEquatable<Term>
    {
        [XmlText]
        public List<string> Text { get; set; }

        public Term()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Term);
        }

        public bool Equals(Term other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Text == null ? (other.Text == null) : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    [Serializable]
    [XmlType("description")]
    public class Description : IEquatable<Description>
    {
        [XmlText]
        public List<string> Text { get; set; }

        public Description()
        {
            Text = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            return Text != null ? Text.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Description);
        }

        public bool Equals(Description other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Text == null ? (other.Text == null) : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    #endregion

#endregion
}
