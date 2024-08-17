namespace server.API.Entities;

public class TodoListItem
{
    public int Id { get; set; }
    public required string Description { get; set; }
    public bool Completed { get; set; } = false;
    public int TodoListId { get; set; }
    public TodoList? TodoList { get; set; }
}
