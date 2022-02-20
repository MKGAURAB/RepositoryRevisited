// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

var uow = new CommandRepoFacede("connection string");
var balanceQueryRepository = new balanceQueryRepository("connection string");

var balanceService = new BalanceService(uow, balanceQueryRepository);
var accountService = new AccountsService(balanceQueryRepository);
