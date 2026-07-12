using PeachPlanner.Infrastructure.Models;

namespace PeachPlanner.Dtos.Todo;

public record UpdateTodo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}