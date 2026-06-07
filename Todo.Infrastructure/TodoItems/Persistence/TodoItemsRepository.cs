namespace Todo.Infrastructure.TodoItems.Persistence;

using Microsoft.Data.Sqlite;

using Todo.Infrastructure.Common.Persistence;

public class TodoItemsRepository : ITodoItemsRepository
{
    private readonly UnitOfWork _unitOfWork;

    public TodoItemsRepository(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(TodoItem item)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.Transaction = _unitOfWork.StartTransaction;
        command.CommandText = """
            INSERT INTO TodoItems (Id, Title, Description, CategoryId, Priority, DueDate, Status, CreatedAt)
            VALUES (@Id, @Title, @Description, @CategoryId, @Priority, @DueDate, @Status, @CreatedAt);
            """;

        AddTodoItemParameters(command, item);
        await command.ExecuteNonQueryAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.CommandText = """
            SELECT Id, Title, Description, CategoryId, Priority, DueDate, Status, CreatedAt
            FROM TodoItems
            WHERE Id = @Id;
            """;
        command.Parameters.AddWithValue("@Id", id.ToString());

        using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return MapTodoItem(reader);
    }

    public async Task<List<TodoItem>> ListAsync(Guid? categoryId = null, TodoItemStatus? status = null)
    {
        using var command = _unitOfWork.Connection.CreateCommand();

        var sql = """
            SELECT Id, Title, Description, CategoryId, Priority, DueDate, Status, CreatedAt
            FROM TodoItems
            WHERE 1 = 1
            """;

        if (categoryId.HasValue)
        {
            sql += " AND CategoryId = @CategoryId";
            command.Parameters.AddWithValue("@CategoryId", categoryId.Value.ToString());
        }

        if (status.HasValue)
        {
            sql += " AND Status = @Status";
            command.Parameters.AddWithValue("@Status", (int)status.Value);
        }

        sql += " ORDER BY CreatedAt DESC";
        command.CommandText = sql;

        var items = new List<TodoItem>();

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            items.Add(MapTodoItem(reader));
        }

        return items;
    }

    public async Task UpdateAsync(TodoItem item)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.Transaction = _unitOfWork.StartTransaction;
        command.CommandText = """
            UPDATE TodoItems
            SET Title = @Title,
                Description = @Description,
                CategoryId = @CategoryId,
                Priority = @Priority,
                DueDate = @DueDate,
                Status = @Status
            WHERE Id = @Id;
            """;

        AddTodoItemParameters(command, item);
        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var command = _unitOfWork.Connection.CreateCommand();
        command.Transaction = _unitOfWork.StartTransaction;
        command.CommandText = "DELETE FROM TodoItems WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id.ToString());
        await command.ExecuteNonQueryAsync();
    }

    private static void AddTodoItemParameters(SqliteCommand command, TodoItem item)
    {
        command.Parameters.AddWithValue("@Id", item.Id.ToString());
        command.Parameters.AddWithValue("@Title", item.Title);
        command.Parameters.AddWithValue("@Description", (object?)item.Description ?? DBNull.Value);
        command.Parameters.AddWithValue("@CategoryId", item.CategoryId.ToString());
        command.Parameters.AddWithValue("@Priority", (int)item.Priority);
        command.Parameters.AddWithValue("@DueDate", item.DueDate.HasValue
            ? item.DueDate.Value.ToString("O")
            : (object)DBNull.Value);
        command.Parameters.AddWithValue("@Status", (int)item.Status);
        command.Parameters.AddWithValue("@CreatedAt", item.CreatedAt.ToString("O"));
    }

    private static TodoItem MapTodoItem(SqliteDataReader reader)
    {
        var id = Guid.Parse(reader.GetString(0));
        var title = reader.GetString(1);
        var description = reader.IsDBNull(2) ? null : reader.GetString(2);
        var categoryId = Guid.Parse(reader.GetString(3));
        var priority = (TodoPriority)reader.GetInt32(4);
        DateTime? dueDate = reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5));
        var status = (TodoItemStatus)reader.GetInt32(6);
        var createdAt = DateTime.Parse(reader.GetString(7));

        return TodoItem.FromPersistence(
            id,
            title,
            description,
            categoryId,
            priority,
            dueDate,
            status,
            createdAt);
    }
}
