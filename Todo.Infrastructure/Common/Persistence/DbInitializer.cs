namespace Todo.Infrastructure.Common.Persistence;

using Microsoft.Data.Sqlite;

public class DbInitializer
{
    private readonly ISqliteConnectionFactory _connectionFactory;

    private static readonly (string Name, Guid Id)[] DefaultCategories =
    {
        ("Работа", Guid.Parse("11111111-1111-1111-1111-111111111111")),
        ("Дом", Guid.Parse("22222222-2222-2222-2222-222222222222")),
        ("Учеба", Guid.Parse("33333333-3333-3333-3333-333333333333"))
    };

    public DbInitializer(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void Initialize()
    {
        using var connection = _connectionFactory.CreateConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Categories (
                Id   TEXT PRIMARY KEY,
                Name TEXT NOT NULL UNIQUE
            );

            CREATE TABLE IF NOT EXISTS TodoItems (
                Id          TEXT PRIMARY KEY,
                Title       TEXT NOT NULL,
                Description TEXT,
                CategoryId  TEXT NOT NULL REFERENCES Categories(Id),
                Priority    INTEGER NOT NULL,
                DueDate     TEXT,
                Status      INTEGER NOT NULL,
                CreatedAt   TEXT NOT NULL
            );
            """;
        command.ExecuteNonQuery();

        using (var walCommand = connection.CreateCommand())
        {
            walCommand.CommandText = "PRAGMA journal_mode=WAL;";
            walCommand.ExecuteNonQuery();
        }

        foreach (var (name, id) in DefaultCategories)
        {
            using var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = """
                INSERT OR IGNORE INTO Categories (Id, Name)
                VALUES (@Id, @Name);
                """;
            insertCommand.Parameters.AddWithValue("@Id", id.ToString());
            insertCommand.Parameters.AddWithValue("@Name", name);
            insertCommand.ExecuteNonQuery();
        }
    }
}
