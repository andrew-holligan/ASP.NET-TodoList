using server.API.DTOs;
using server.API.Entities;

namespace server.API.Mapping;

public static class TodoListMapping
{
    public static TodoList ToEntity(this CreateTodoListDTO todolist)
    {
        return new TodoList()
        {
            Name = todolist.Name
        };
    }

    public static TodoList ToEntity(this UpdateTodoListDTO todolist, int id)
    {
        return new TodoList()
        {
            Id = id,
            Name = todolist.Name
        };
    }

    public static TodoListDTO ToDTO(this TodoList todolist)
    {
        return new TodoListDTO(
            todolist.Id,
            todolist.Name
        );
    }
}
