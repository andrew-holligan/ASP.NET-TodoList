using System.ComponentModel.DataAnnotations;

namespace server.API.DTOs;

public record class UpdateTodoListDTO([Required][StringLength(50)] string Name);
