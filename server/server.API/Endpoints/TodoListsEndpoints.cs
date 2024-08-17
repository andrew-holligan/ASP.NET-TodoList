using server.API.DTOs;

namespace server.API.Endpoints;

public static class TodoListsEndpoints
{
    const string EndpointNameGetTodoList = "GetTodoList";
    private static readonly List<TodoListDTO> todolists = [];

    public static RouteGroupBuilder MapTodoListsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todolists").WithParameterValidation();

        // GET /todolists
        group.MapGet("/", () => todolists);

        // GET /todolists/{id}
        group.MapGet("/{id}", (int id) =>
        {
            TodoListDTO? todolist = todolists.Find(todolist => todolist.Id == id);
            return todolist is null ? Results.NotFound() : Results.Ok(todolist);
        })
            .WithName(EndpointNameGetTodoList);

        // POST /todolists
        group.MapPost("/", (CreateTodoListDTO newTodoList) =>
        {
            TodoListDTO todolist = new TodoListDTO(todolists.Count + 1, newTodoList.Name);
            todolists.Add(todolist);
            return Results.CreatedAtRoute(EndpointNameGetTodoList, new { id = todolist.Id }, todolist);
        });

        // PUT /todolists/{id}
        group.MapPut("/{id}", (int id, UpdateTodoListDTO updatedTodoList) =>
        {
            var index = todolists.FindIndex(todolist => todolist.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            todolists[index] = new TodoListDTO(id, updatedTodoList.Name);
            return Results.NoContent();
        });

        // DELETE /todolists/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            todolists.RemoveAll(todolist => todolist.Id == id);
            return Results.NoContent();
        });

        return group;
    }
}
