using Microsoft.EntityFrameworkCore;
using PeachPlanner.Infrastructure.Models;

namespace PeachPlanner.Infrastructure;

public class PlannerDbContext: DbContext
{
    
    public PlannerDbContext(DbContextOptions<PlannerDbContext> options)
        : base(options) { }
    
    public DbSet<Todo> Todos => Set<Todo>();
} 