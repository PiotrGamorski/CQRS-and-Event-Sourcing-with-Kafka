using CQRS.Core;
using Microsoft.Extensions.DependencyInjection;
using Post.Command.Api;
using Post.Command.Application;
using Post.Command.Application.Commands.AddCommentCommand;
using Post.Command.Application.Commands.DeletePostCommand;
using Post.Command.Application.Commands.EditCommentCommand;
using Post.Command.Application.Commands.EditMessageCommand;
using Post.Command.Application.Commands.LikePostCommand;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.Commands.RemoveCommentCommand;
using Post.Command.Infrastructure;
using Post.Command.Infrastructure.Dispatchers;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

    var serviceProvider = builder.Services.BuildServiceProvider();
    var dispatcher = new CommandDispatcher();
 
    dispatcher.RegisterHandler<NewPostCommand>(serviceProvider.GetRequiredService<INewPostCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<LikePostCommand>(serviceProvider.GetRequiredService<ILikePostCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<AddCommentCommand>(serviceProvider.GetRequiredService<IAddCommentCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<EditMessageCommand>(serviceProvider.GetRequiredService<IEditMessageCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<EditCommentCommand>(serviceProvider.GetRequiredService<IEditCommentCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<RemoveCommentCommand>(serviceProvider.GetRequiredService<IRemoveCommentCommandHandler>().HandleAsync);
    dispatcher.RegisterHandler<DeletePostCommand>(serviceProvider.GetRequiredService<IDeletePostCommandHandler>().HandleAsync);

    builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);



    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
