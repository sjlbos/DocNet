
using System.Collections.Generic;

namespace DocNet.Core.Models.CSharp
{
    public interface IParentElement: IEnumerable<INestableElement>
    {
        string FullName { get; }
        INestableElement this[string uniqueName] { get; }

        void AddChild(INestableElement child);
        bool HasDirectDescendant(INestableElement child);
        bool HasDescendant(INestableElement child);
        bool NestedElementIsLegal(INestableElement element);
    }
}
