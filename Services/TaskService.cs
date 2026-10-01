using TaskBoard.Models;

namespace TaskBoard.Services;

/// <summary>
/// Archivio in memoria delle attività. Registrato come singleton:
/// i dati sono condivisi tra tutte le sessioni e si azzerano al riavvio.
/// </summary>
public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public TaskService()
    {
        Seed("Preparare la demo", isDone: true);
        Seed("Risolvere la issue #1", isDone: false);
        Seed("Risolvere la issue #2", isDone: false);
    }

    public IReadOnlyList<TaskItem> GetAll()
    {
        lock (_lock)
        {
            return _tasks.OrderBy(t => t.Id).ToList();
        }
    }

    /// <summary>
    /// Crea una nuova attività. Restituisce null se il titolo non è valido.
    /// </summary>
    public TaskItem? Add(string? title)
    {
        lock (_lock)
        {
            var task = new TaskItem
            {
                Id = _nextId++,
                Title = title?.Trim() ?? string.Empty
            };
            _tasks.Add(task);
            return task;
        }
    }

    public bool Toggle(int id)
    {
        lock (_lock)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task is null) return false;
            task.IsDone = !task.IsDone;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_lock)
        {
            return _tasks.RemoveAll(t => t.Id == id) > 0;
        }
    }

    private void Seed(string title, bool isDone)
    {
        _tasks.Add(new TaskItem
        {
            Id = _nextId++,
            Title = title,
            IsDone = isDone
        });
    }
}
