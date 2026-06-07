namespace Todo.Application.TodoItems.Commands.UpdateTodoItem;

using ErrorOr;

using MediatR;

public class UpdateTodoItemCommand : IRequest<ErrorOr<TodoItem>>
{
    public Guid Id { get; }
    public string Title { get; }
    public Guid CategoryId { get; }
    public TodoPriority Priority { get; }
    public DateTime? DueDate { get; }
    public string? Description { get; }

    public UpdateTodoItemCommand(
        Guid id,
        string title,
        Guid categoryId,
        TodoPriority priority,
        DateTime? dueDate = null,
        string? description = null)
    {
        Id = id;
        Title = title;
        CategoryId = categoryId;
        Priority = priority;
        DueDate = dueDate;
        Description = description;
    }
}
