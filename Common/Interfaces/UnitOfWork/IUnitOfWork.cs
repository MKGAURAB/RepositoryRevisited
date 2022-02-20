public interface IUnitOfWork : IDisposable
{
    void Commit(string changeUser);
    Task CommitAsync(string changeUser);
    IDisposable BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}