using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;

namespace server.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TodoListContext>();
        dbContext.Database.Migrate();
    }
}
