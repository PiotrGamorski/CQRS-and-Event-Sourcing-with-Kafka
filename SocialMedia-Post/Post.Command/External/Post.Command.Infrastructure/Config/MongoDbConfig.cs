namespace Post.Command.Infrastructure.Config
{
    public sealed class MongoDbConfig
    {
        public const string SectionName = nameof(MongoDbConfig);
        public string ConnectionString { get; init; } = null!;
        public string Database { get; init; } = null!;
        public string Collection { get; init; } = null!;
    }
}
