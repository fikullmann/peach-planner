using PeachPlanner.Infrastructure.Models;

namespace PeachPlanner.Dtos.Todo;

public record GetTodo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TodoState TodoState { get; set; }
    public bool IsDeleted { get; set; }
}