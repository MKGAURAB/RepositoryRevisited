public interface IBalanceQueryRepository
{
    public Task<IEnumerable<Balance>> GetBalancesForAccountsAync(string accountId, DateTime balanceDate);
    public Task<Balance> GetBalanceAsync(string balanceId);
}