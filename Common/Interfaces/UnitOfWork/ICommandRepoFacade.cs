public interface ICommandRepoFacade : IUnitOfWork
{
    ICommandRepository<Account> AccountRepository {get;}
    ICommandRepository<Balance> BalanceRepository {get;}
    ICommandRepository<Transaction> TransactionRepository {get;}
}