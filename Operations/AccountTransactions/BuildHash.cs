namespace BankApi.Operations.AccountTransactions;

using BankApi.Models;

public class BuildHash
{
    public Dictionary<string, object> Hash { get; private set; }
    public AccountTransaction AccountTransaction { get; private set; }

    public BuildHash(AccountTransaction accountTransaction)
    {
        this.AccountTransaction = accountTransaction;
        this.Hash = new Dictionary<string, object>();
    }

    public void run()
    {
        Hash["amount"] = AccountTransaction.Amount;
        Hash["transactionType"] = AccountTransaction.TransactionType;
        Hash["transactedAt"] = AccountTransaction.TransactedAt;
        Hash["startingBalance"] = AccountTransaction.StartingBalance;
        Hash["endingBalance"] = AccountTransaction.EndingBalance;
    }
}