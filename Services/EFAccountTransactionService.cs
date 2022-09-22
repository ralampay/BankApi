namespace BankApi.Services;

using System.Collections.Generic;
using BankApi.Models;

public class EFAccountTransactionService : IAccountTransactionService
{
    public List<AccountTransaction> GetAllByBankAccount(int id)
    {
        throw new NotImplementedException();
    }

    public AccountTransaction Transact(BankAccount bankAccount, string transactionType, decimal amount)
    {
        throw new NotImplementedException();
    }
}