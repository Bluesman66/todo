namespace Todo.Application.TodoItems.Commands.CreateTodoItem;

using ErrorOr;

using MediatR;

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, ErrorOr<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoItemCommandHandler(
        ITodoItemsRepository todoItemsRepository,
        ICategoriesRepository categoriesRepository,
        IUnitOfWork unitOfWork)
    {
        _todoItemsRepository = todoItemsRepository;
        _categoriesRepository = categoriesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<TodoItem>> Handle(CreateTodoItemCommand command, CancellationToken cancellationToken)
    {
        if (!await _categoriesRepository.ExistsAsync(command.CategoryId))
        {
            return CategoryErrors.NotFound;
        }

        var todoItem = new TodoItem(
            title: command.Title,
            categoryId: command.CategoryId,
            priority: command.Priority,
            dueDate: command.DueDate,
            description: command.Description);

        await _todoItemsRepository.AddAsync(todoItem);
        await _unitOfWork.CommitChangesAsync();

        return todoItem;
    }
}
