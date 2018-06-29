using System.Collections.Generic;

namespace CrowdFunding.Core.Models
{
    public partial class Category
    {
        public Category()
        {
            Project = new HashSet<Project>();
        }

        public long CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Project> Project { get; set; }
    }
}
