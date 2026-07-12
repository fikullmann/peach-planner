using Mapster;
using Microsoft.EntityFrameworkCore;
using PeachPlanner.Dtos.Todo;
using PeachPlanner.Infrastructure;
using PeachPlanner.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<PlannerDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/todoitems", async (PlannerDbContext db) =>
    (await db.Todos.ProjectToType<GetTodo>().ToListAsync()));

app.MapGet("/todoitems/complete", async (PlannerDbContext db) =>
    await db.Todos.Where(t => t.TodoState == TodoState.Completed).ProjectToType<GetTodo>().ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, PlannerDbContext db) =>
    (await db.Todos.FindAsync(id)).Adapt<GetTodo>()
        is GetTodo todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/todoitems", async (CreateTodo createTodo, PlannerDbContext db) =>
{
    var todo = createTodo.Adapt<Todo>();
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", createTodo);
});

app.MapPut("/todoitems/{id}", async (int id, UpdateTodo inputTodo, PlannerDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    inputTodo.Adapt(todo);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, PlannerDbContext db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();