using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRS.Core.Events
{
    public class EventModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; } = null!;
        public DateTime TimeStamp { get; init; }
        public Guid AggregateIdentifier { get; init; }
        public string AggregateType { get; init; } = null!;
        public int Version { get; init; }
        public string EventType { get; init; } = null!;
        public BaseEvent EventData { get; init; } = null!;
    }
}
