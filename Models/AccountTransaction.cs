namespace BankApi.Models;

public class AccountTransaction
{
    public Int32 Id {get; set;}
    public String TransactionType {get; set;}
    public Decimal Amount { get; set;}
    public BankAccount BankAccount {get; set;}
    public DateTime TransactedAt { get; set; }
    public Decimal StartingBalance { get; set; }
    public Decimal EndingBalance { get; set; }
}