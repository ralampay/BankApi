namespace BankApi.Services;

using BankApi.Models;

public interface IAccountTransactionService
{
    public AccountTransaction Transact(BankAccount bankAccount, String transactionType, Decimal amount);
    public List<AccountTransaction> GetAllByBankAccount(int id);
}