using System.Data.Entity;
using System.Data.SqlClient;
using Dapper;

public class balanceQueryRepository : IBalanceQueryRepository
{
    private readonly string _connectionString;

    public balanceQueryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Balance> GetBalanceAsync(string balanceId)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QueryFirstOrDefaultAsync<Balance>("select * from Position where BalanceId = @BalanceId",
                new { BalanceId = balanceId }).ConfigureAwait(false);
        }
    }

    // Using Dapper
    public async Task<IEnumerable<Balance>> GetBalancesForAccountsAync(string accountId, DateTime balanceDate)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QueryAsync<Balance>("select * from Position where AccountCode = @AccountId and BalanceDate = @BalanceDate",
                new { AccountId = accountId, BalanceDate = balanceDate }).ConfigureAwait(false);
        }
    }
    // using EF6
    public async Task<List<Balance>> EF_GetPositionsForAccountAsync(string accountId, DateTime balanceDate)
    {
        using (var context = new EFDbContext(_connectionString))
        {
            return await context.Balances.Where(p => p.AccountId == accountId && p.BalanceDate == balanceDate)
                .ToListAsync().ConfigureAwait(false);
        }
    }

}