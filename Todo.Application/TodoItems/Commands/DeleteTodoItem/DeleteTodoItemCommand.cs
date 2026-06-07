namespace Todo.Application.TodoItems.Commands.DeleteTodoItem;

using ErrorOr;

using MediatR;

public class DeleteTodoItemCommand : IRequest<ErrorOr<Deleted>>
{
    public Guid Id { get; }

    public DeleteTodoItemCommand(Guid id)
    {
        Id = id;
    }
}
