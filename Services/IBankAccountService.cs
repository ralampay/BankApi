namespace BankApi.Services;

using BankApi.Models;

public interface IBankAccountService
{
    public List<BankAccount> GetAll();
}