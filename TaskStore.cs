namespace TasksMcpServer;

public record TaskItem(int Id, string Title, string Description, bool IsComplete, DateTime CreatedAt);

public class TaskStore
{
    private readonly List<TaskItem> _tasks = new()
    {
        new(1, "Buy groceries", "Milk, eggs, bread", false, DateTime.UtcNow),
        new(2, "Write docs", "Draft the MCP tutorial", true, DateTime.UtcNow.AddDays(-1)),
    };

    private int _nextId = 3;

    public List<TaskItem> GetAll() => _tasks.ToList();

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public TaskItem Create(string title, string description)
    {
        var task = new TaskItem(_nextId++, title, description, false, DateTime.UtcNow);
        _tasks.Add(task);
        return task;
    }

    public TaskItem? ToggleComplete(int id)
    {
        var index = _tasks.FindIndex(t => t.Id == id);
        if (index < 0) return null;
        var old = _tasks[index];
        var updated = old with { IsComplete = !old.IsComplete };
        _tasks[index] = updated;
        return updated;
    }

    public bool Delete(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task is null) return false;
        _tasks.Remove(task);
        return true;
    }
}