namespace BankApi.Services;

using BankApi.Models;

public interface IATMCardService
{
    public ATMCard FindById(int id);
}