namespace Post.Command.Infrastructure.Config
{
    public sealed class MongoDbConfig
    {
        public const string SectionName = nameof(MongoDbConfig);
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";
        public string Database { get; set; } = null!;
        public string Collection { get; set; } = null!;
    }
}
