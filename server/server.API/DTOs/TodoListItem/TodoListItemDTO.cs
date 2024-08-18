namespace server.API.DTOs;

public record class TodoListItemDTO(int Id, string Description, bool Completed, int TodoListId);
