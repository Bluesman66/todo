namespace Todo.Application.TodoItems.Queries.GetTodoItemById;

using ErrorOr;

using MediatR;

public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, ErrorOr<TodoItem>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public GetTodoItemByIdQueryHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }

    public async Task<ErrorOr<TodoItem>> Handle(GetTodoItemByIdQuery query, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemsRepository.GetByIdAsync(query.Id);

        if (todoItem is null)
        {
            return TodoErrors.NotFound;
        }

        return todoItem;
    }
}
