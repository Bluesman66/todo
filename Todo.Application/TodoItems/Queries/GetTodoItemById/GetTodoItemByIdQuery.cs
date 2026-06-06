namespace Todo.Application.TodoItems.Queries.GetTodoItemById;

using ErrorOr;

using MediatR;

public class GetTodoItemByIdQuery : IRequest<ErrorOr<TodoItem>>
{
    public Guid Id { get; }

    public GetTodoItemByIdQuery(Guid id)
    {
        Id = id;
    }
}
