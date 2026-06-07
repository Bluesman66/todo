namespace Todo.Application.Common.Interfaces;

public interface ICategoriesRepository
{
    Task<List<Category>> ListAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
