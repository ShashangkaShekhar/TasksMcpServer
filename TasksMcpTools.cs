using System.ComponentModel;
using ModelContextProtocol.Server;

namespace TasksMcpServer;

[McpServerToolType]
public class TasksMcpTools
{
    private readonly TaskStore _store;

    public TasksMcpTools(TaskStore store)
    {
        _store = store;
    }

    [McpServerTool, Description("Lists all tasks with their ID, title, description, and completion status.")]
    public List<TaskItem> ListTasks()
    {
        return _store.GetAll();
    }

    [McpServerTool, Description("Gets a single task by its ID.")]
    public TaskItem? GetTask(
        [Description("The numeric ID of the task to retrieve")] int id)
    {
        return _store.GetById(id);
    }

    [McpServerTool, Description("Creates a new task with the given title and description. Returns the created task.")]
    public TaskItem CreateTask(
        [Description("A short title for the task")] string title,
        [Description("A detailed description of what the task involves")] string description)
    {
        return _store.Create(title, description);
    }

    [McpServerTool, Description("Toggles a task's completion status between complete and incomplete.")]
    public string ToggleTaskComplete(
        [Description("The numeric ID of the task to toggle")] int id)
    {
        var task = _store.ToggleComplete(id);
        return task is not null
            ? $"Task {task.Id} is now {(task.IsComplete ? "complete" : "incomplete")}."
            : $"Task with ID {id} not found.";
    }

    [McpServerTool, Description("Deletes a task by its ID.")]
    public string DeleteTask(
        [Description("The numeric ID of the task to delete")] int id)
    {
        return _store.Delete(id)
            ? $"Task {id} deleted."
            : $"Task with ID {id} not found.";
    }
}