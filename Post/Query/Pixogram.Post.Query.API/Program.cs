using Pixogram.Post.Query.API.Endpoints;
using Pixogram.Post.Query.Application;
using Pixogram.Post.Query.Infrastructure;

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
