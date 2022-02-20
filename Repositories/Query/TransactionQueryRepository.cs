using System.Data.SqlClient;
using Dapper;

public class TransactionQueryRepository : ITransactionQueryRepository
{
    private readonly string _connectionString;

    public TransactionQueryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsForAccountsAsync(string accountId, DateTime transactionDate)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            return await conn.QueryAsync<Transaction>("select * from Transaction where  AccountId = @AccountId and TransactionDate = @TransactionDate",
                new { AccountId = accountId, TransactionDate = transactionDate }).ConfigureAwait(false);
        }
    }
}