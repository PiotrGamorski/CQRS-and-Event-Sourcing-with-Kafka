using Confluent.Kafka;
using CQRS.Core.Application.Events;
using CQRS.Core.Application.Persistance.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Post.Command.Infrastructure.Config;

namespace Post.Command.Infrastructure.Repositories
{
    public sealed class EventStoreRepository : IEventStoreRepository
    {
        private readonly IMongoCollection<EventModel> _eventStoreCollection;

        public EventStoreRepository(IOptions<MongoDbConfig> config) 
        {
            InitializeMongoDb(config, out var eventStoreCollection);
            _eventStoreCollection = eventStoreCollection;
        }

        public async Task<List<EventModel>> FindByAggreagateIdAsync(Guid aggregateId)
        {
            return await _eventStoreCollection
                .Find(x => x.AggregateIdentifier == aggregateId)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task SaveAsync(EventModel @event)
        {
            await _eventStoreCollection
                .InsertOneAsync(@event)
                .ConfigureAwait(false);
        }

        private void InitializeMongoDb(IOptions<MongoDbConfig> config, out IMongoCollection<EventModel> eventStoreCollection)
        {
            var connectionString = config.Value.ConnectionString;
            eventStoreCollection = null!;
            IMongoDatabase? mongoDatabase = null;

            if (connectionString == null)
            {
                eventStoreCollection = null!;
                return;
            }
               
            if (TryEstablishMongoDbConnection(connectionString, out var mongoClient))
            {
                mongoDatabase = mongoClient.GetDatabase(config.Value.Database);
            }
            else if (TryEstablishMongoDbConnection("mongodb://localhost:27017", out var fallbackMongoClient))
            {
                mongoDatabase = fallbackMongoClient.GetDatabase(config.Value.Database);
            }

            if (mongoDatabase != null)
            {
                eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
            }
        }

        private bool TryEstablishMongoDbConnection(string connectionString, out MongoClient mongoClient)
        {
            try
            {
                mongoClient = new MongoClient(connectionString);
                return true;
            }
            catch (Exception)
            {
                mongoClient = null!;
                return false;
            }  
        }
    }
}
