namespace Todo.Application.TodoItems.Commands.DeleteTodoItem;

using ErrorOr;

using MediatR;

public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, ErrorOr<Deleted>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(
        ITodoItemsRepository todoItemsRepository,
        IUnitOfWork unitOfWork)
    {
        _todoItemsRepository = todoItemsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteTodoItemCommand command, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemsRepository.GetByIdAsync(command.Id);

        if (todoItem is null)
        {
            return TodoErrors.NotFound;
        }

        await _todoItemsRepository.DeleteAsync(command.Id);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
