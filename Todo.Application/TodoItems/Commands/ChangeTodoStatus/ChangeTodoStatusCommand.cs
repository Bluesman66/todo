namespace Todo.Application.TodoItems.Commands.ChangeTodoStatus;

using ErrorOr;

using MediatR;

public class ChangeTodoStatusCommand : IRequest<ErrorOr<TodoItem>>
{
    public Guid Id { get; }
    public TodoItemStatus Status { get; }

    public ChangeTodoStatusCommand(Guid id, TodoItemStatus status)
    {
        Id = id;
        Status = status;
    }
}
