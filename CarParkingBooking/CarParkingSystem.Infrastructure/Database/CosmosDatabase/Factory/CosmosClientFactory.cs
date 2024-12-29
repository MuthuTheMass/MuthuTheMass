using CarParkingSystem.Domain.Entities.Cosmos;
using Microsoft.Azure.Cosmos;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace CarParkingSystem.Infrastructure.Database.CosmosDatabase.Factory
{
    public interface ICosmosClientFactory
    {
        Task<Container> GetOrCreateContainerAsync(string databaseName, string containerName, string partitionKeyPath);

        Task<string> GetNextBookingIdAsync(string container);
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

        public async Task<string> GetNextBookingIdAsync(string counterId)
        {
            Container container;
            try
            {
                container = await GetOrCreateContainerAsync("counters", "AutoIncreament", "/PartitionId");


                // Query to find the counter document
                var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                    .WithParameter("@id", counterId);

                // Execute the query
                var iterator = container.GetItemQueryIterator<CosmosCounter>(query);
                var result = await iterator.ReadNextAsync();

                // If counter document doesn't exist, create it
                CosmosCounter counter = result.Count > 0 ? result.First() : new CosmosCounter { id = counterId, currentValue = 1,PartitionId = counterId };

                // Increment the current value
                var newBookingId = $"booking-{counter.currentValue}";

                // Increment the counter value and update the document
                counter.currentValue++;

                // Use PartitionKey correctly when performing Upsert
                var partitionKey = new PartitionKey(counter.id);  // Using the 'id' as the partition key

                // Perform the upsert operation with the correct PartitionKey
                await container.UpsertItemAsync<CosmosCounter>(counter, partitionKey);
                container = null;
                return newBookingId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating booking ID: {ex.Message}");
                return null;
            }
        }
    }
}
