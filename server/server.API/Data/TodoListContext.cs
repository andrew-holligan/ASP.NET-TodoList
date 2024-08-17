using Microsoft.EntityFrameworkCore;
using server.API.Entities;

namespace server.API.Data;

public class TodoListContext(DbContextOptions<TodoListContext> options) : DbContext(options)
{
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoListItem> TodoListItems => Set<TodoListItem>();
}
