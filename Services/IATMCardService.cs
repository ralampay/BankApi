namespace BankApi.Services;

using BankApi.Models;

public interface IATMCardService
{
    public ATMCard FindById(int id);

    public List<ATMCard> FindByCustomerId(int customerId);

    public ATMCard FindByIdAndCustomerId(int id, int customerId);

    public ATMCard Save(ATMCard atmCard);

    public bool Delete(ATMCard atmCard);
}