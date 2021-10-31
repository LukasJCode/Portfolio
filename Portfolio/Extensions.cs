using Portfolio.Models;

namespace Portfolio
{
    public static class Extensions
    {
        public static ProjectDto AsDto(this Project project)
        {
            return new ProjectDto(project.Id, project.Title, project.About, project.Implementation, project.Conclusion, project.CreatedDate);
        }
    }
}