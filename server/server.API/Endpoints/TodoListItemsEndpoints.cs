using Microsoft.EntityFrameworkCore;
using server.API.Data;
using server.API.DTOs;
using server.API.Entities;
using server.API.Mapping;

namespace server.API.Endpoints;

public static class TodoListItemsEndpoints
{
    const string EndpointNameGetTodoListItem = "GetTodoListItem";

    public static RouteGroupBuilder MapTodoListItemsEndpoints(this RouteGroupBuilder todolistGroup)
    {
        var group = todolistGroup.MapGroup("items")
            .WithParameterValidation();

        // Validation Middleware
        group.AddEndpointFilter(async (EndpointFilterInvocationContext context, EndpointFilterDelegate next) =>
        {
            // Get todolist id
            string? idStr = context.HttpContext.Request.RouteValues["id"] as string;
            if (!int.TryParse(idStr, out var id)) return Results.BadRequest();


            // Retrieve the dbContext
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<TodoListContext>();

            // Check that the todolist exists
            TodoList? todolist = await dbContext.TodoLists
                .FindAsync(id);

            if (todolist is null) return Results.NotFound();


            return await next(context);
        });


        // GET /todolists/{id}/items
        group.MapGet("/", async (int id, TodoListContext dbContext) =>
            await dbContext.TodoListItems
                .Where(item => item.TodoListId == id)
                .Select(item => item.ToDTO())
                .AsNoTracking()
                .ToListAsync()
        );

        // GET /todolists/{id}/items/{itemId}
        group.MapGet("/{itemId}", async (int id, int itemId, TodoListContext dbContext) =>
        {
            TodoListItem? item = await dbContext.TodoListItems
                .FindAsync(itemId);

            return item is null ? Results.NotFound() : Results.Ok(item.ToDTO());
        })
        .WithName(EndpointNameGetTodoListItem);

        // POST /todolists/{id}/items
        group.MapPost("/", async (int id, CreateTodoListItemDTO newItem, TodoListContext dbContext) =>
        {
            TodoListItem item = newItem.ToEntity(id);

            dbContext.TodoListItems.Add(item);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(EndpointNameGetTodoListItem, new { id = item.TodoListId, itemId = item.Id }, item.ToDTO());
        });

        // PUT /todolists/{id}/items/{itemId}
        group.MapPut("/{itemId}", async (int id, int itemId, UpdateTodoListItemDTO updatedItem, TodoListContext dbContext) =>
        {
            var existingItem = await dbContext.TodoListItems
                .FindAsync(itemId);

            if (existingItem is null) return Results.NotFound();

            dbContext.Entry(existingItem)
                .CurrentValues
                .SetValues(updatedItem.ToEntity(id, itemId));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /todolists/{id}/items/{itemId}
        group.MapDelete("/{itemId}", async (int id, int itemId, TodoListContext dbContext) =>
        {
            await dbContext.TodoListItems
                .Where(item => item.Id == itemId)
                .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
