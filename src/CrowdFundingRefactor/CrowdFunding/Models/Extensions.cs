using CrowdFunding.Core.Models;

namespace CrowdFunding.Models
{
    public static class Extensions
    {
        public static ProjectViewModel ToViewModel(this Project project)
        {
            var url = project.PictureUrl ?? "projects/not-available.jpg";

            return new ProjectViewModel {
                CategoryId = project.CategoryId,
                Deadline = project.Deadline,
                Description = project.Description,
                Goal = project.Goal,
                PersonId = project.PersonId,
                PictureUrl = url,
                Title = project.Title,
                Category = project.Category,
                Person = project.Person,
                ProjectId = project.ProjectId
            };
        }

        public static Project ToModel(this ProjectViewModel project)
        {
            return new Project {
                CategoryId = project.CategoryId,
                Deadline = project.Deadline,
                Description = project.Description,
                Goal = project.Goal,
                Category = project.Category,
                PersonId = project.PersonId,
                Title = project.Title,
                Person = project.Person,
                ProjectId = project.ProjectId
            };
        }
    }
}
