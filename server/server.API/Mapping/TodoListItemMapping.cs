using server.API.DTOs;
using server.API.Entities;

namespace server.API.Mapping;

public static class TodoListItemMapping
{
    public static TodoListItem ToEntity(this CreateTodoListItemDTO item, int todolistId)
    {
        return new TodoListItem()
        {
            Description = item.Description,
            Completed = item.Completed,
            TodoListId = todolistId
        };
    }

    public static TodoListItem ToEntity(this UpdateTodoListItemDTO item, int todolistId, int id)
    {
        return new TodoListItem()
        {
            Id = id,
            Description = item.Description,
            Completed = item.Completed,
            TodoListId = todolistId
        };
    }

    public static TodoListItemDTO ToDTO(this TodoListItem item)
    {
        return new TodoListItemDTO(
            item.Id,
            item.Description,
            item.Completed,
            item.TodoListId
        );
    }
}