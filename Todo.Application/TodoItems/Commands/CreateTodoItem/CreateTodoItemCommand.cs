namespace Todo.Application.TodoItems.Commands.CreateTodoItem;

using ErrorOr;

using MediatR;

public class CreateTodoItemCommand : IRequest<ErrorOr<TodoItem>>
{
    public string Title { get; }
    public Guid CategoryId { get; }
    public TodoPriority Priority { get; }
    public DateTime? DueDate { get; }
    public string? Description { get; }

    public CreateTodoItemCommand(
        string title,
        Guid categoryId,
        TodoPriority priority,
        DateTime? dueDate = null,
        string? description = null)
    {
        Title = title;
        CategoryId = categoryId;
        Priority = priority;
        DueDate = dueDate;
        Description = description;
    }
}
