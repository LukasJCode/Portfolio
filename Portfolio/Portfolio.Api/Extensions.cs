using Portfolio.Api.Models;

namespace Portfolio.Api
{
    public static class Extensions
    {
        public static ProjectDto AsDto(this Project project)
        {
            return new ProjectDto(project.Id, project.Title, project.About, project.Implementation, project.Conclusion, project.CreatedDate);
        }
    }
}