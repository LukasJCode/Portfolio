using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio.Api.Models;

namespace Portfolio.Api.Repositories
{
    public interface IProjectsRepository
    {
        Task<Project> GetProjectAsync(Guid id);
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
        Task DeleteAllProjectsAsync();
    }
}