using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Post.Command.Infrastructure.Config;

namespace Post.Command.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {  
            services.AddMongoDbConfig(configuration);
            return services;
        }

        private static IServiceCollection AddMongoDbConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            var mongoDbConfig = new MongoDbConfig();
            configuration.Bind(MongoDbConfig.SectionName, mongoDbConfig);
            services.AddSingleton(Options.Create(mongoDbConfig));

            return services;
        }
    }
}
