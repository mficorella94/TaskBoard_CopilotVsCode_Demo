namespace TaskBoard.Models;

public class TaskItem
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}

public record CreateTaskRequest(string? Title);
