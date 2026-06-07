namespace Todo.Infrastructure.Common.Persistence;

using Microsoft.Data.Sqlite;

public class SqliteConnectionFactory : ISqliteConnectionFactory
{
    public string ConnectionString { get; }

    public SqliteConnectionFactory(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(ConnectionString);
    }
}
