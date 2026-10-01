using TaskBoard.Components;
using TaskBoard.Models;
using TaskBoard.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<TaskService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// --- Minimal API ---------------------------------------------------------
var api = app.MapGroup("/api/tasks");

api.MapGet("/", (TaskService service) => service.GetAll());

api.MapPost("/", (CreateTaskRequest request, TaskService service) =>
{
    var task = service.Add(request.Title);
    return task is null
        ? Results.BadRequest(new { error = "Il titolo dell'attività è obbligatorio." })
        : Results.Created($"/api/tasks/{task.Id}", task);
});

api.MapPatch("/{id:int}/toggle", (int id, TaskService service) =>
    service.Toggle(id) ? Results.NoContent() : Results.NotFound());

api.MapDelete("/{id:int}", (int id, TaskService service) =>
    service.Delete(id) ? Results.NoContent() : Results.NotFound());

// --- Blazor --------------------------------------------------------------
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
