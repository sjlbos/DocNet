using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Parameter : TopLevelContainer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlType("typeparam")]
    public class TypeParameter : TopLevelContainer
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlType("exception")]
    public class ExceptionReference : TopLevelContainer
    {
        [XmlAttribute("cref")]
        public string ExceptionType { get; set; }
    }

    [Serializable]
    [XmlType("seealso")]
    public class SeeAlsoReference
    {
        public SeeAlsoReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("cref")]
        public string ReferencedElementName { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }
    }

    [Serializable]
    [XmlType("permission")]
    public class PermissionReference
    {
        public PermissionReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("cref")]
        public string ReferencedElementName { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }
    }

    [Serializable]
    [XmlType("include")]
    public class ExternalDocFileReference
    {
        [XmlAttribute("file")]
        public string FileName { get; set; }

        [XmlAttribute("path")]
        public string FilePath { get; set; }
    }

    #endregion

    #region Child Elements

    [Serializable]
    [XmlType("para")]
    public class Paragraph : Container { }

    [Serializable]
    [XmlType("see")]
    public class SeeReference
    {
        public SeeReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("cref")]
        public string ReferencedElementName { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }
    }

    [Serializable]
    [XmlType("c")]
    public class CodeLine
    {
        public CodeLine()
        {
            Text = new List<string>();
        }

        [XmlText]
        public IList<string> Text;
    }

    [Serializable]
    [XmlType("code")]
    public class CodeBlock
    {
        public CodeBlock()
        {
            Text = new List<string>();
        }

        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlText]
        public IList<string> Text;
    }

    [Serializable]
    [XmlType("paramref")]
    public class ParameterReference
    {
        public ParameterReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }
    }

    [Serializable]
    [XmlType("typeparamref")]
    public class TypeParameterReference
    {
        public TypeParameterReference()
        {
            Text = new List<string>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public IList<string> Text { get; set; }
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
    public class DocList
    {
        [XmlAttribute("type")]
        public ListType Type { get; set; }

        public ListHeader Header { get; set; }

        public IList<ListItem> Elements { get; set; }

        public DocList()
        {
            Elements = new List<ListItem>();
        }
    }

    public abstract class ListElement
    {
        public IList<Term> Terms { get; set; }
        public IList<Description> Descriptions { get; set; }

        protected ListElement()
        {
            Terms = new List<Term>();
            Descriptions = new List<Description>();
        }
    }

    [Serializable]
    [XmlType("listheader")]
    public class ListHeader : ListElement { }

    [Serializable]
    [XmlType("item")]
    public class ListItem : ListElement { }

    [Serializable]
    [XmlType("term")]
    public class Term
    {
        [XmlText]
        public IList<string> Text { get; set; }

        public Term()
        {
            Text = new List<string>();
        }
    }

    [Serializable]
    [XmlType("description")]
    public class Description
    {
        [XmlText]
        public IList<string> Text { get; set; }

        public Description()
        {
            Text = new List<string>();
        }
    }

    #endregion

#endregion

}
