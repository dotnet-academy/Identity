using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CrowdFunding.Core.Models
{
    public partial class Person : IdentityUser<long>
    {
        public Person()
        {
            Projects = new HashSet<Project>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfileUrl { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
