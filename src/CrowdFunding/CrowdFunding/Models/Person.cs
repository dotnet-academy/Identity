using System;
using System.Collections.Generic;

namespace CrowdFunding.Models
{
    public partial class Person
    {
        public Person()
        {
            Project = new HashSet<Project>();
        }

        public long PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string ProfileUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Project> Project { get; set; }
    }
}
