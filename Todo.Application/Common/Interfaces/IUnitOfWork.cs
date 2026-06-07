namespace Todo.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
