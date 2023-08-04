using CQRS.Core;
using CQRS.Core.Application.Persistance;
using CQRS.Core.Application.Persistance.Repositories;
using CQRS.Core.Application.Producers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Post.Command.Application.Commands.AddCommentCommand;
using Post.Command.Application.Commands.DeletePostCommand;
using Post.Command.Application.Commands.EditCommentCommand;
using Post.Command.Application.Commands.EditMessageCommand;
using Post.Command.Application.Commands.LikePostCommand;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.Commands.RemoveCommentCommand;
using Post.Command.Application.Handlers;
using Post.Command.Application.Stores;
using Post.Command.Domain.Aggregates;
using Post.Command.Infrastructure.Config;
using Post.Command.Infrastructure.Dispatchers;
using Post.Command.Infrastructure.Producers;
using Post.Command.Infrastructure.Repositories;

namespace Post.Command.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMongoDbConfig(configuration);
            services.AddProducerConfig(configuration);
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventProducer, EventProducer>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventSourcingHandler<PostAggregate>, EventSourcingHandler>();

            return services;
        }

        private static IServiceCollection AddMongoDbConfig(this IServiceCollection services, ConfigurationManager configuration)
        {
            var mongoDbConfig = new MongoDbConfig();
            configuration.Bind(MongoDbConfig.SectionName, mongoDbConfig);
            services.AddSingleton(Options.Create(mongoDbConfig));

            return services;
        }

        private static IServiceCollection AddProducerConfig(this IServiceCollection services, ConfigurationManager configuration)
        { 
            var producerConfig = new ProducerConfig();
            configuration.Bind(producerConfig);
            services.AddSingleton(Options.Create(producerConfig));

            return services;
        }
    }
}
