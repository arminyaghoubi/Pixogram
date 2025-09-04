using Pixogram.Post.Command.Infrastructure;
using Pixogram.Post.Command.API.Endpoints;
using Pixogram.Post.Command.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapPostEndpoints()
   .MapCommentEndpoints();

app.Run();
