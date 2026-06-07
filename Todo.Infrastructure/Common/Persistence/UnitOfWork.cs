namespace Todo.Infrastructure.Common.Persistence;

using Microsoft.Data.Sqlite;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ISqliteConnectionFactory _connectionFactory;
    private SqliteConnection? _connection;
    private SqliteTransaction? _transaction;

    public UnitOfWork(ISqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public SqliteConnection Connection => EnsureConnection();

    public SqliteTransaction StartTransaction
    {
        get
        {
            EnsureTransaction();
            return _transaction!;
        }
    }

    public Task CommitChangesAsync()
    {
        if (_transaction is null)
        {
            return Task.CompletedTask;
        }

        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;

        return Task.CompletedTask;
    }

    private SqliteConnection EnsureConnection()
    {
        if (_connection is null)
        {
            _connection = _connectionFactory.CreateConnection();
            _connection.Open();
        }
        return _connection;
    }

    private void EnsureTransaction()
    {
        EnsureConnection();
        _transaction ??= _connection!.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}
