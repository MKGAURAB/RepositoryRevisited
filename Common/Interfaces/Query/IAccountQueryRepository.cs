public interface IAccountQueryRepository
{
    public Task<IEnumerable<Account>> GetAccountsAsync();
}