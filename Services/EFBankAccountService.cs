namespace BankApi.Services;

using System.Collections.Generic;
using BankApi.Models;

public class EFBankAccountService : IBankAccountService
{
    public BankAccount Find(int id)
    {
        throw new NotImplementedException();
    }

    public BankAccount FindByAccountNumber(string accountNumber)
    {
        throw new NotImplementedException();
    }

    public BankAccount FindById(int id)
    {
        throw new NotImplementedException();
    }

    public List<BankAccount> GetAccounts(int customerId)
    {
        throw new NotImplementedException();
    }

    public List<BankAccount> GetAccounts(string refNumber)
    {
        throw new NotImplementedException();
    }

    public List<Dictionary<string, object>> GetAccountsPayload(int customerId)
    {
        throw new NotImplementedException();
    }

    public List<BankAccount> GetAll()
    {
        throw new NotImplementedException();
    }

    public BankAccount UpdateBalance(BankAccount bankAccount, decimal balance)
    {
        throw new NotImplementedException();
    }
}