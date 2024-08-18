using System.ComponentModel.DataAnnotations;

namespace server.API.DTOs;

public record class CreateTodoListItemDTO([Required][StringLength(200)] string Description, [Required] bool Completed);
