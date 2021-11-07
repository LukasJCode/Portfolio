using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Portfolio.Api;
using Portfolio.Api.Controllers;
using Portfolio.Api.Models;
using Portfolio.Api.Repositories;
using Xunit;

namespace Portfolio.UnitTests
{
    public class ProjectsControllerTests
    {
        private readonly Mock<IProjectsRepository> repositoryStub = new();

        [Fact]
        public async Task GetProjectAsync_WithUnexistingProject_ReturnsNotFound()
        {
            //Arrange
            repositoryStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Project)null);

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
            var result = await controller.GetProjectAsync(Guid.NewGuid());

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetProjectAsync_WithExistingProject_ReturnsExpectedItem()
        {
            //Arrange
            var expectedProject = CreateRandomProject();

            repositoryStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>()))
            .ReturnsAsync((expectedProject));

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
            var result = await controller.GetProjectAsync(Guid.NewGuid());

            //Assert
            result.Value.Should().BeEquivalentTo(expectedProject, options => options.ComparingByMembers<Project>());

        }

         [Fact]
        public async Task GetProjectsAsync_WithExistingProjects_ReturnsAllProjects()
        {
            //Arrange
            var expectedProjects = new[]{CreateRandomProject(), CreateRandomProject(), CreateRandomProject()};

            repositoryStub.Setup(repo => repo.GetProjectsAsync())
            .ReturnsAsync((expectedProjects));

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
            var actualItems = await controller.GetProjectAsync();

            //Assert
            actualItems.Should().BeEquivalentTo(
                expectedProjects,
                options => options.ComparingByMembers<Project>()
            );
        }

        
         [Fact]
        public async Task CreateProjectsAsync_WIthItemToCreate_ReturnsCreatedItem()
        {
            //Arrange
            var projectToCreate = new CreateProjectDto(
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
           var result = await controller.CreateProjectAsync(projectToCreate);

            //Assert
            var createdProject = (result.Result as CreatedAtActionResult).Value as ProjectDto;
            projectToCreate.Should().BeEquivalentTo(
                createdProject,
                options => options.ComparingByMembers<ProjectDto>().ExcludingMissingMembers()
            );
            createdProject.Id.Should().NotBeEmpty();
            createdProject.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, new TimeSpan(0, 0, 1));
        }

         [Fact]
        public async Task UpdateProjectAsync_WithExistingItem_ReturnsNoContent()
        {
            //Arrange
            var existingProject = CreateRandomProject();
            repositoryStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>()))
            .ReturnsAsync((existingProject));

            var projectId = existingProject.Id;
            var projectToUpdate = new UpdateProjectDto(
                 Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString());

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
            var result = await controller.UpdateProjectAsync(projectId, projectToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteProjectAsync_WithExistingItem_ReturnsNoContent()
        {
            //Arrange
            var existingProject = CreateRandomProject();
            repositoryStub.Setup(repo => repo.GetProjectAsync(It.IsAny<Guid>()))
            .ReturnsAsync((existingProject));

            var controller = new ProjectsController(repositoryStub.Object);

            //Act
            var result = await controller.DeleteProjectAsync(existingProject.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private Project CreateRandomProject()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                About = Guid.NewGuid().ToString(),
                Implementation = Guid.NewGuid().ToString(),
                Conclusion = Guid.NewGuid().ToString(),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
