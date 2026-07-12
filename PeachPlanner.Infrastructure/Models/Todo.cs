namespace PeachPlanner.Infrastructure.Models;

public record Todo()
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TodoState TodoState { get; set; }
    public bool IsDeleted { get; set; }
};
