using PeachPlanner.Infrastructure.Models;

namespace PeachPlanner.Dtos.Todo;

public record CreateTodo
{
    public string Title { get; set; }
    public string Description { get; set; }
}