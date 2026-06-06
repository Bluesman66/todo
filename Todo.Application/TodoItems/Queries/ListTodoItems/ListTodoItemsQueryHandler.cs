namespace Todo.Application.TodoItems.Queries.ListTodoItems;

using ErrorOr;

using MediatR;

public class ListTodoItemsQueryHandler : IRequestHandler<ListTodoItemsQuery, ErrorOr<List<TodoItem>>>
{
    private readonly ITodoItemsRepository _todoItemsRepository;

    public ListTodoItemsQueryHandler(ITodoItemsRepository todoItemsRepository)
    {
        _todoItemsRepository = todoItemsRepository;
    }

    public async Task<ErrorOr<List<TodoItem>>> Handle(ListTodoItemsQuery query, CancellationToken cancellationToken)
    {
        var items = await _todoItemsRepository.ListAsync(query.CategoryId, query.Status);
        return items;
    }
}
