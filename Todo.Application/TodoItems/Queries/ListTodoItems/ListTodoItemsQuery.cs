namespace Todo.Application.TodoItems.Queries.ListTodoItems;

using ErrorOr;

using MediatR;

public class ListTodoItemsQuery : IRequest<ErrorOr<List<TodoItem>>>
{
    public Guid? CategoryId { get; }
    public TodoItemStatus? Status { get; }

    public ListTodoItemsQuery(Guid? categoryId = null, TodoItemStatus? status = null)
    {
        CategoryId = categoryId;
        Status = status;
    }
}
