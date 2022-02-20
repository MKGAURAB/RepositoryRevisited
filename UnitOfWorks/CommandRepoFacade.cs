using System.Data.Entity;

public class CommandRepoFacede : EFDbContext, ICommandRepoFacade
{
    private readonly Lazy<ICommandRepository<Account>> accountCommandRepo;
    private readonly Lazy<ICommandRepository<Balance>> balanceCommandRepo;
    private readonly Lazy<ICommandRepository<Transaction>> transactionCommandRepo;

    public CommandRepoFacede(string connectionString) : base(connectionString)
    {
        accountCommandRepo = new Lazy<ICommandRepository<Account>>(() => new EFCommandRepository<Account>(this));
        balanceCommandRepo = new Lazy<ICommandRepository<Balance>>(() => new EFCommandRepository<Balance>(this));
        transactionCommandRepo = new Lazy<ICommandRepository<Transaction>>(() => new EFCommandRepository<Transaction>(this));
    }

    public ICommandRepository<Account> AccountRepository
    {
        get
        {
            return accountCommandRepo.Value;
        }
    }
    public ICommandRepository<Balance> BalanceRepository
    {
        get
        {
            return balanceCommandRepo.Value;
        }
    }

    public ICommandRepository<Transaction> TransactionRepository
    {
        get
        {
            return transactionCommandRepo.Value;
        }
    }

    public void Commit(string changeUser)
    {
        SaveChanges(changeUser);
    }

    public Task CommitAsync(string changeUser)
    {
        return SaveChangesAsync(changeUser);
    }
    #region EF stuff
    /// <remarks>EF6 will check for migration history on every DB call unless the initializer is set to null in the most-derived class.</remarks>
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        Database.SetInitializer<CommandRepoFacede>(null);
        base.OnModelCreating(modelBuilder);
    }
    #endregion
}
