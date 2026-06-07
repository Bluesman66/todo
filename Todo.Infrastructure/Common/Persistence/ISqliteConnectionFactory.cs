namespace Todo.Infrastructure.Common.Persistence;

using Microsoft.Data.Sqlite;

public interface ISqliteConnectionFactory
{
    string ConnectionString { get; }
    SqliteConnection CreateConnection();
}
