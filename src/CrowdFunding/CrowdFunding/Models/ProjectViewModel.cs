using CrowdFunding.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace CrowdFunding.Models
{
    public partial class ProjectViewModel
    {
        public long ProjectId { get; set; }

        public long PersonId { get; set; }

        public long CategoryId { get; set; }

        public IFormFile Picture { get; set; }

        public string PictureUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        public decimal Goal { get; set; }

        public Category Category { get; set; }

        public Person Person { get; set; }
    }
}
