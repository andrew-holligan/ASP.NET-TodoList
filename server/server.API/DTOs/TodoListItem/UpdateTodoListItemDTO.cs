using System.ComponentModel.DataAnnotations;

namespace server.API.DTOs;

public record class UpdateTodoListItemDTO([Required][StringLength(200)] string Description, [Required] bool Completed);
