using server.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapTodoListsEndpoints();
app.Run();
