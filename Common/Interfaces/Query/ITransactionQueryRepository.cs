public interface ITransactionQueryRepository
{
    public Task<IEnumerable<Transaction>> GetTransactionsForAccountsAsync(string accountId, DateTime transactionDate);
}