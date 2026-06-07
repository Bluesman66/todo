namespace Todo.Application.Common.Interfaces;

public interface ITodoItemsRepository
{
    Task AddAsync(TodoItem item);
    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<List<TodoItem>> ListAsync(Guid? categoryId = null, TodoItemStatus? status = null);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(Guid id);
}
