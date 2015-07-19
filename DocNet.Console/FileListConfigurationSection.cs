using System.Configuration;

namespace DocNet.Console
{
    public class FileListConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public FileListCollection Files
        {
            get { return (FileListCollection) this[""]; }
            set { this[""] = value; }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface"), ConfigurationCollection(typeof(FileListCollection), AddItemName = "file")]
    public class FileListCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileElement) element).Name;
        }
    }

    public class FileElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name {
            get { return (string) base["name"]; }
            set { base["name"] = value; } 
        }
    }
}
