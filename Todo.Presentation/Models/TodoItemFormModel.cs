namespace Todo.Presentation.Models;

using Todo.Domain.TodoItems;

public sealed class TodoItemFormModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public TodoPriority Priority { get; set; }
    public TodoItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public bool HasDueDate { get; set; }
}
