using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio
{
    public record ProjectDto(Guid Id, string Title, string About, string Implementation, string Conclusion, DateTimeOffset CreatedDate);
    public record CreateProjectDto([Required]string Title, [Required]string About, [Required]string Implementation, [Required]string Conclusion);
    public record UpdateProjectDto([Required]string Title, [Required]string About, [Required]string Implementation, [Required]string Conclusion);

}