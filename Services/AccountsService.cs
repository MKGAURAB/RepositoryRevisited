public class AccountsService
{
    private readonly IBalanceQueryRepository _balanceQueryRepository;

    public AccountsService(IBalanceQueryRepository balanceQueryRepository)
    {
        _balanceQueryRepository = balanceQueryRepository;
    }

    public async Task<decimal> CalculateDailyProfit(string accountId, DateTime balanceDate)
    {
        var todaysBalance = _balanceQueryRepository.GetBalancesForAccountsAync(accountId, balanceDate.Date);
        var yesterdaysBalance = _balanceQueryRepository.GetBalancesForAccountsAync(accountId, balanceDate.AddDays(-1).Date);
        
        await Task.WhenAll(todaysBalance, yesterdaysBalance).ConfigureAwait(false);

        return todaysBalance.Result.Sum(p => p.Amount) - yesterdaysBalance.Result.Sum(p => p.Amount);
    }
}