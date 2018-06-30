using System;

namespace CrowdFunding.Core.Models
{
    public partial class Project
    {
        public long ProjectId { get; set; }

        public long PersonId { get; set; }
        public long CategoryId { get; set; }
        public string PictureUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public decimal Goal { get; set; }

        public Category Category { get; set; }
        public Person Person { get; set; }
    }
}
