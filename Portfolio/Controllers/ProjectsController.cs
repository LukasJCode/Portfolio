using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Repositories;

namespace Portfolio.Controllers
{
    // GET /projects
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository repository;

        public ProjectsController(IProjectsRepository repository)
        {
            this.repository = repository;
        }

        //Get /projects
        [HttpGet]
        public async Task<IEnumerable<ProjectDto>> GetProjectAsync()
        {
            var projects = (await repository.GetProjectsAsync()).Select(project => project.AsDto());

            return projects;
        }

        //Get /projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectAsync(Guid id)
        {
            var project = await repository.GetProjectAsync(id);

            if(project is null)
            {
                return NotFound();
            }

            return project.AsDto();
        }

        //Post /projects
        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProjectAsync(CreateProjectDto projectDto)
        {
            Project newProject = new()
            {
                Id = Guid.NewGuid(),
                Title = projectDto.Title,
                About = projectDto.About,
                Implementation = projectDto.Implementation,
                Conclusion = projectDto.Conclusion,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateProjectAsync(newProject);
            //return NoContent();
            return CreatedAtAction(nameof(GetProjectAsync), new{ id = newProject.Id}, newProject.AsDto());
        }

        //Put /projects/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectAsync(Guid id, UpdateProjectDto projectDto)
        {
            var existingProject = await repository.GetProjectAsync(id);

            if(existingProject is null)
            {
                return NotFound();
            }

            existingProject.Title = projectDto.Title;
            existingProject.About = projectDto.About;
            existingProject.Implementation = projectDto.Implementation;
            existingProject.Conclusion = projectDto.Conclusion;

            await repository.UpdateProjectAsync(existingProject);
            
            return NoContent();
        }

        //Delete /projects/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(Guid id)
        {
            var existingProject = repository.GetProjectAsync(id);

            if(existingProject is null)
            {
                return NotFound();
            }

            await repository.DeleteProjectAsync(id);
            
            return NoContent();
        }

        //Delete /projects
        [HttpDelete]
        public async Task<ActionResult> DeleteAllProjectAsync()
        {
            await repository.DeleteAllProjectsAsync();
            return NoContent();
        }
    }
}