using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Xml.Serialization;

namespace DocNet.Models.Comments
{
    public abstract class Container : IEquatable<Container>
    {   
        [XmlText]
        [XmlElement("c")]
        [XmlElement("code")]
        [XmlElement("list")]
        [XmlElement("paramref")]
        [XmlElement("see")]
        [XmlElement("typeparamref")]
        public IList<object> Items { get; set; }

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
            return Items == null ? (other.Items == null) : Items.SequenceEqual(other.Items);
        }

        #endregion
    }

    public abstract class ReferenceElement : IEquatable<ReferenceElement>
    {
        [XmlAttribute("cref")]
        public string ReferencedElementName { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }

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

    public abstract class TopLevelContainer : Container
    {
        [XmlElement("para")]
        public new IList<object> Items { get; set; }
    }

    [Serializable]
    [XmlType("summary")]
    public class Summary : TopLevelContainer { }

    [Serializable]
    [XmlType("remarks")]
    public class Remarks : TopLevelContainer { }

    [Serializable]
    [XmlType("example")]
    public class Example : TopLevelContainer { }

    [Serializable]
    [XmlType("returns")]
    public class Returns : TopLevelContainer { }

    [Serializable]
    [XmlType("value")]
    public class Value : TopLevelContainer { }

    [Serializable]
    [XmlType("param")]
    public class Parameter : TopLevelContainer, IEquatable<Parameter>
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
            return Equals(obj as Parameter);
        }

        public bool Equals(Parameter other)
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
    public class TypeParameter : TopLevelContainer, IEquatable<TypeParameter>
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
            return Equals(obj as TypeParameter);
        }

        public bool Equals(TypeParameter other)
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
    public class ExceptionReference : TopLevelContainer, IEquatable<ExceptionReference>
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
            return Equals(obj as ExceptionReference);
        }

        public bool Equals(ExceptionReference other)
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
    public class SeeAlsoReference : ReferenceElement { }

    [Serializable]
    [XmlType("permission")]
    public class PermissionReference : ReferenceElement { }

    [Serializable]
    [XmlType("include")]
    public class ExternalDocFileReference
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
            return Equals(obj as ExternalDocFileReference);
        }

        public bool Equals(ExternalDocFileReference other)
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
    public class Paragraph : Container { }

    [Serializable]
    [XmlType("see")]
    public class SeeReference : ReferenceElement { }

    [Serializable]
    [XmlType("c")]
    public class CodeLine : IEquatable<CodeLine>
    {
        [XmlText]
        public IList<string> Text;

        public CodeLine()
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
            return Equals(obj as CodeLine);
        }

        public bool Equals(CodeLine other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return Text == null ? (other.Text == null) : Text.SequenceEqual(other.Text);
        }

        #endregion
    }

    [Serializable]
    [XmlType("code")]
    public class CodeBlock : IEquatable<CodeBlock>
    {
        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlText]
        public IList<string> Text;

        public CodeBlock()
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
            return Equals(obj as CodeBlock);
        }

        public bool Equals(CodeBlock other)
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
    public class ParameterReference : IEquatable<ParameterReference>
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }

        public ParameterReference()
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
            return Equals(obj as ParameterReference);
        }

        public bool Equals(ParameterReference other)
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
    public class TypeParameterReference : IEquatable<TypeParameterReference>
    {
        public TypeParameterReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }

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
            return Equals(obj as TypeParameterReference);
        }

        public bool Equals(TypeParameterReference other)
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
    public class DocumentationList : IEquatable<DocumentationList>
    {
        [XmlAttribute("type")]
        public ListType ListType { get; set; }

        public ListHeader Header { get; set; }

        public IList<ListItem> Elements { get; set; }

        public DocumentationList()
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
            return Equals(obj as DocumentationList);
        }

        public bool Equals(DocumentationList other)
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
        public IList<Term> Terms { get; set; }
        public IList<Description> Descriptions { get; set; }

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
        public IList<string> Text { get; set; }

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
        public IList<string> Text { get; set; }

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
