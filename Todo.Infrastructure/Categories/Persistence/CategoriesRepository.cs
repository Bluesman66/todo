namespace Todo.Infrastructure.Categories.Persistence;

using Microsoft.Data.Sqlite;

using Todo.Infrastructure.Common.Persistence;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly UnitOfWork _unitOfWork;

    public CategoriesRepository(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Category>> ListAsync()
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.CommandText = "SELECT Id, Name FROM Categories ORDER BY Name";

        var categories = new List<Category>();

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            categories.Add(MapCategory(reader));
        }

        return categories;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.CommandText = "SELECT Id, Name FROM Categories WHERE Id = @Id";
        command.Parameters.AddWithValue("@Id", id.ToString());

        using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return MapCategory(reader);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.CommandText = "SELECT 1 FROM Categories WHERE Id = @Id LIMIT 1";
        command.Parameters.AddWithValue("@Id", id.ToString());

        var result = await command.ExecuteScalarAsync();
        return result is not null;
    }

    private static Category MapCategory(SqliteDataReader reader)
    {
        var id = Guid.Parse(reader.GetString(0));
        var name = reader.GetString(1);
        return Category.FromPersistence(id, name);
    }
}
