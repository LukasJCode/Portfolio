using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Portfolio.Models;

namespace Portfolio.Repositories
{
    public class MongoDbProjectsRepository : IProjectsRepository
    {
        private const string databaseName = "portfolio";
        private const string collectionName = "projects";
        private readonly IMongoCollection<Project> projectsCollection;
        private readonly FilterDefinitionBuilder<Project> filterBuilder = Builders<Project>.Filter;

        public MongoDbProjectsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            projectsCollection = database.GetCollection<Project>(collectionName);
        }

        public async Task CreateProjectAsync(Project project)
        {
            await projectsCollection.InsertOneAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            var filter = filterBuilder.Eq(project => project.Id, id);
            await projectsCollection.DeleteOneAsync(filter);
        }

        public async Task<Project> GetProjectAsync(Guid id)
        {
            var filter = filterBuilder.Eq(project => project.Id, id);
            return await projectsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await projectsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            var filter = filterBuilder.Eq(existingProject => existingProject.Id, project.Id);
            await projectsCollection.ReplaceOneAsync(filter, project);
        }
    }
}