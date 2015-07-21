
using System.Collections.Generic;

namespace DocNet.Core.Models.CSharp
{
    public interface IParentElement: IEnumerable<INestableElement>
    {
        string DisplayName { get; }
        string FullNameQualifier { get; }
        string FullDisplayName { get; }
        string InternalName { get; }
        string FullInternalName { get; }

        INestableElement this[string internalName] { get; }

        void AddChild(INestableElement child);
        bool HasDirectDescendant(INestableElement child);
        bool HasDescendant(INestableElement child);
        bool NestedElementIsLegal(INestableElement element);
    }
}
