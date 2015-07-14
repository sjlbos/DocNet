using System;
using System.Collections.Generic;
using System.Linq;

namespace DocNet.Core.Models.VisualStudio
{
    public class ProjectModel : IEquatable<ProjectModel>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IList<string> IncludedFilePaths { get; set; }

        public ProjectModel()
        {
            IncludedFilePaths = new List<string>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode*397) ^ (FilePath != null ? FilePath.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (IncludedFilePaths != null ? IncludedFilePaths.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ProjectModel);
        }

        public bool Equals(ProjectModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name) &&
                   String.Equals(FilePath, other.FilePath) &&
                   ListsContainSameElements(IncludedFilePaths, other.IncludedFilePaths);
        }

        private static bool ListsContainSameElements<T>(IList<T> a, IList<T> b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            return a.Count == b.Count &&
                   !a.Except(b).Any();
        }

        #endregion
    }
}
