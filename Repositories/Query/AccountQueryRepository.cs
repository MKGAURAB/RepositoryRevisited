using System.Data.Entity;
public class AccountQueryRepository : IAccountQueryRepository
{
    private readonly string _connectionString;

    public AccountQueryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync()
    {
        using (var context = new EFDbContext(_connectionString))
        {
            return await context.Accounts.ToListAsync().ConfigureAwait(false);
        }
    }
}