using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CrowdFunding.Models
{
    public partial class Person : IdentityUser<long>
    {
        public Person()
        {
            Project = new HashSet<Project>();
        }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfileUrl { get; set; }

        public ICollection<Project> Project { get; set; }
    }
}
