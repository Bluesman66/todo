namespace Todo.Application.TodoItems.Commands.UpdateTodoItem;

using ErrorOr;

using MediatR;

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, ErrorOr<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoItemCommandHandler(
        ITodoItemsRepository todoItemsRepository,
        ICategoriesRepository categoriesRepository,
        IUnitOfWork unitOfWork)
    {
        _todoItemsRepository = todoItemsRepository;
        _categoriesRepository = categoriesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TodoItem>> Handle(UpdateTodoItemCommand command, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemsRepository.GetByIdAsync(command.Id);

        if (todoItem is null)
        {
            return TodoErrors.NotFound;
        }

        if (!await _categoriesRepository.ExistsAsync(command.CategoryId))
        {
            return CategoryErrors.NotFound;
        }

        var updateResult = todoItem.UpdateDetails(
            command.Title,
            command.Description,
            command.CategoryId,
            command.Priority,
            command.DueDate);

        if (updateResult.IsError)
        {
            return updateResult.Errors;
        }

        await _todoItemsRepository.UpdateAsync(todoItem);
        await _unitOfWork.CommitChangesAsync();

        return todoItem;
    }
}
