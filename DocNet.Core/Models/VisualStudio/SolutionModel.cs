using System;
using System.Collections.Generic;
using System.Linq;

namespace DocNet.Core.Models.VisualStudio
{
    public class SolutionModel : IEquatable<SolutionModel>
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public IList<ProjectModel> Projects { get; set; }

        public SolutionModel()
        {
            Projects = new List<ProjectModel>();
        }

        #region Equality Members

        public override int GetHashCode()
        {
            int hashCode = Name != null ? Name.GetHashCode() : 0;
            hashCode = (hashCode*397) ^ (FilePath != null ? FilePath.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (Projects != null ? Projects.GetHashCode() : 0);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SolutionModel);
        }

        public bool Equals(SolutionModel other)
        {
            if (other == null) return false;
            if (this == other) return true;
            return String.Equals(Name, other.Name) &&
                   String.Equals(FilePath, other.FilePath) &&
                   ProjectListsContainSameElements(Projects, other.Projects);
        }

        private static bool ProjectListsContainSameElements(IList<ProjectModel> a, IList<ProjectModel> b)
        {
            if (a == b) return true;
            if (a == null || b == null) return false;
            return a.OrderBy(p => p.Name).SequenceEqual(b.OrderBy(p => p.Name));
        }

        #endregion
    }
}
