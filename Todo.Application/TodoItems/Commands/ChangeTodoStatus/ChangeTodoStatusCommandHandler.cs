namespace Todo.Application.TodoItems.Commands.ChangeTodoStatus;

using ErrorOr;

using MediatR;

public class ChangeTodoStatusCommandHandler : IRequestHandler<ChangeTodoStatusCommand, ErrorOr<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeTodoStatusCommandHandler(
        ITodoItemsRepository todoItemsRepository,
        IUnitOfWork unitOfWork)
    {
        _todoItemsRepository = todoItemsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TodoItem>> Handle(ChangeTodoStatusCommand command, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemsRepository.GetByIdAsync(command.Id);

        if (todoItem is null)
        {
            return TodoErrors.NotFound;
        }

        var changeResult = todoItem.ChangeStatus(command.Status);

        if (changeResult.IsError)
        {
            return changeResult.Errors;
        }

        await _todoItemsRepository.UpdateAsync(todoItem);
        await _unitOfWork.CommitChangesAsync();

        return todoItem;
    }
}
