namespace Todo.Presentation.Models;

using Todo.Domain.TodoItems;

public readonly struct MainViewFilterState
{
    public Guid? CategoryId { get; }
    public TodoItemStatus? Status { get; }

    public MainViewFilterState(Guid? categoryId, TodoItemStatus? status)
    {
        CategoryId = categoryId;
        Status = status;
    }
}
