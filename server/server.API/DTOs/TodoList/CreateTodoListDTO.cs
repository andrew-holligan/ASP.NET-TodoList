using System.ComponentModel.DataAnnotations;

namespace server.API.DTOs;

public record class CreateTodoListDTO([Required][StringLength(50)] string Name);
