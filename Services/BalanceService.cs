public class BalanceService
{
    private readonly ICommandRepoFacade _commandRepoFacade;
    private readonly IBalanceQueryRepository _balanceQueryRepository;
    public BalanceService(ICommandRepoFacade crf, IBalanceQueryRepository bqr)
    {
        _commandRepoFacade = crf;
        _balanceQueryRepository = bqr;
    }

    public void AddOrUpdateBalance(Transaction trxn)
    {
        var balance = _balanceQueryRepository.GetBalanceAsync(trxn.AccountId).Result;
        if (balance == null)
        {
            CreateBalance(trxn);
        }
        else UpdateBalance(balance, trxn);

        _commandRepoFacade.Commit("ConsoleUser");
    }

    private void UpdateBalance(Balance balance, Transaction trxn)
    {
        balance.Amount += trxn.Amount;
        _commandRepoFacade.BalanceRepository.Update(balance);
    }

    private void CreateBalance(Transaction trxn)
    {
        _commandRepoFacade.TransactionRepository.Add(trxn);
        var balance = new Balance()
        {
            AccountId = trxn.AccountId,
            BalanceDate = trxn.TransactionDate,
            Amount = trxn.Amount
        };
        _commandRepoFacade.BalanceRepository.Add(balance);
    }
}