using server.API.Data;
using server.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TodoList");
builder.Services.AddSqlite<TodoListContext>(connectionString);

var app = builder.Build();
app.MapTodoListsEndpoints();

app.MigrateDb();
app.Run();
