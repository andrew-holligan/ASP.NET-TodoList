using Microsoft.EntityFrameworkCore;
using server.API.Data;
using server.API.DTOs;
using server.API.Entities;
using server.API.Mapping;

namespace server.API.Endpoints;

public static class TodoListsEndpoints
{
    const string EndpointNameGetTodoList = "GetTodoList";

    public static RouteGroupBuilder MapTodoListsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("todolists")
            .WithParameterValidation();

        // GET /todolists
        group.MapGet("/", async (TodoListContext dbContext) =>
            await dbContext.TodoLists
                .Select(todolist => todolist.ToDTO())
                .AsNoTracking()
                .ToListAsync()
        );

        // GET /todolists/{id}
        var todolistWithIdGroup = group.MapGroup("/{id}")
            .WithParameterValidation();

        todolistWithIdGroup.MapGet("/", async (int id, TodoListContext dbContext) =>
        {
            TodoList? todolist = await dbContext.TodoLists
                .FindAsync(id);

            return todolist is null ? Results.NotFound() : Results.Ok(todolist.ToDTO());
        })
        .WithName(EndpointNameGetTodoList);

        // Mapping TodoListItems Endpoints
        // /todolists/{id}/items
        todolistWithIdGroup.MapTodoListItemsEndpoints();

        // POST /todolists
        group.MapPost("/", async (CreateTodoListDTO newTodoList, TodoListContext dbContext) =>
        {
            TodoList todolist = newTodoList.ToEntity();

            dbContext.TodoLists.Add(todolist);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(EndpointNameGetTodoList, new { id = todolist.Id }, todolist.ToDTO());
        });

        // PUT /todolists/{id}
        group.MapPut("/{id}", async (int id, UpdateTodoListDTO updatedTodoList, TodoListContext dbContext) =>
        {
            var existingTodoList = await dbContext.TodoLists
                .FindAsync(id);

            if (existingTodoList is null) return Results.NotFound();

            dbContext.Entry(existingTodoList)
                .CurrentValues
                .SetValues(updatedTodoList.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /todolists/{id}
        group.MapDelete("/{id}", async (int id, TodoListContext dbContext) =>
        {
            await dbContext.TodoLists
                .Where(todolist => todolist.Id == id)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
