using Post.Command.Api;
using Post.Command.Application;
using Post.Command.Application.Commands.NewPostCommand;
using Post.Command.Application.Handlers;
using Post.Command.Infrastructure;
using Post.Command.Infrastructure.Dispatchers;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration, builder.Services.BuildServiceProvider())
        .AddApplication();

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
