namespace BankApi.Services;

using BankApi.Models;

public interface IBankAccountService
{
    public List<BankAccount> GetAll();

    public BankAccount FindById(Int32 id);

    public BankAccount FindByAccountNumber(string accountNumber);

    public BankAccount UpdateBalance(BankAccount bankAccount, Decimal balance);

    public List<Dictionary<string, object>> GetAccountsPayload(int customerId);

    public List<BankAccount> GetAccounts(int customerId);

    public List<BankAccount> GetAccounts(string refNumber);
}