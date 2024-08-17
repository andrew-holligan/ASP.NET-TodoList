namespace server.API.DTOs;

public record class TodoListItemTDO(int Id, int TodoListId, string Description, bool Completed);
