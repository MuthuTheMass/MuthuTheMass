using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;

namespace CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory
{
    public interface ICosmosClientFactory
    {
        Task<Container> GetOrCreateContainerAsync(string databaseName, string containerName, string partitionKeyPath);
    }

    public class CosmosClientFactory : ICosmosClientFactory
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ConcurrentDictionary<string, Microsoft.Azure.Cosmos.Database> _databases = new();
        private readonly ConcurrentDictionary<string, Container> _containers = new();

        public CosmosClientFactory(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;

        }

        public async Task<Container> GetOrCreateContainerAsync(string databaseName, string containerName, string partitionKeyPath)
        {
            // Check or add the database
            if (!_databases.TryGetValue(databaseName, out var database))
            {
                // Fetch or create the database
                var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
                database = databaseResponse.Database;

                // Add the database to the cache
                _databases.TryAdd(databaseName, database);
            }

            // Check or add the container
            var containerKey = $"{databaseName}:{containerName}";
            if (!_containers.TryGetValue(containerKey, out var container))
            {
                // Fetch or create the container
                var containerResponse = await database.CreateContainerIfNotExistsAsync(
                    new ContainerProperties
                    {
                        Id = containerName,
                        PartitionKeyPath = partitionKeyPath
                    });

                container = containerResponse.Container;

                // Add the container to the cache
                _containers.TryAdd(containerKey, container);
            }

            return container;
        }
    }
}
