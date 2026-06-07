namespace Todo.Domain.TodoItems;

using ErrorOr;

public class TodoItem
{
    public Guid Id { get; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public Guid CategoryId { get; private set; }
    public TodoPriority Priority { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TodoItemStatus Status { get; private set; }
    public DateTime CreatedAt { get; }

    public TodoItem(
        string title,
        Guid categoryId,
        TodoPriority priority,
        DateTime? dueDate = null,
        string? description = null,
        TodoItemStatus status = TodoItemStatus.Pending,
        Guid? id = null,
        DateTime? createdAt = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Название задачи не может быть пустым.", nameof(title));
        }

        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException("Категория обязательна.", nameof(categoryId));
        }

        Id = id ?? Guid.NewGuid();
        Title = title;
        Description = description;
        CategoryId = categoryId;
        Priority = priority;
        DueDate = dueDate;
        Status = status;
        CreatedAt = createdAt ?? DateTime.UtcNow;
    }

    public ErrorOr<Success> UpdateDetails(
        string title,
        string? description,
        Guid categoryId,
        TodoPriority priority,
        DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            return TodoErrors.TitleRequired;
        }

        if (categoryId == Guid.Empty)
        {
            return TodoErrors.CategoryRequired;
        }

        Title = title;
        Description = description;
        CategoryId = categoryId;
        Priority = priority;
        DueDate = dueDate;

        return Result.Success;
    }

    public ErrorOr<Success> ChangeStatus(TodoItemStatus newStatus)
    {
        if (Status == TodoItemStatus.Completed && newStatus != TodoItemStatus.Completed)
        {
            return TodoErrors.InvalidStatusTransition;
        }

        if (Status == TodoItemStatus.Cancelled && newStatus != TodoItemStatus.Cancelled)
        {
            return TodoErrors.InvalidStatusTransition;
        }

        Status = newStatus;
        return Result.Success;
    }

    public ErrorOr<Success> MarkCompleted()
    {
        if (Status == TodoItemStatus.Completed)
        {
            return TodoErrors.AlreadyCompleted;
        }

        Status = TodoItemStatus.Completed;
        return Result.Success;
    }

    public static TodoItem FromPersistence(
        Guid id,
        string title,
        string? description,
        Guid categoryId,
        TodoPriority priority,
        DateTime? dueDate,
        TodoItemStatus status,
        DateTime createdAt) =>
        new TodoItem(
            title,
            categoryId,
            priority,
            dueDate,
            description,
            status,
            id,
            createdAt);

    private TodoItem() { }
}
