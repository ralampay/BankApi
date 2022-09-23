namespace BankApi.Services;

using System.Collections.Generic;
using BankApi.Models;
using BankApi.Data;

public class EFATMCardService : IATMCardService
{
    private readonly DataContext _dataContext;

    public EFATMCardService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Delete(ATMCard atmCard)
    {
        if(atmCard.Id != null && atmCard.Id > 0) {
            _dataContext.ATMCards.Remove(atmCard);
            _dataContext.SaveChanges();

            return true;
        }

        return false;
    }

    public List<ATMCard> FindByCustomerId(int customerId)
    {
        return _dataContext.ATMCards.Where(c => c.CustomerId == customerId).ToList();
    }

    public ATMCard FindById(int id)
    {
        return _dataContext.ATMCards.SingleOrDefault(c => c.Id == id);
    }

    public ATMCard FindByIdAndCustomerId(int id, int customerId)
    {
        return _dataContext.ATMCards.SingleOrDefault(c => c.Id == id && c.CustomerId == customerId);
    }

    public ATMCard Save(ATMCard atmCard)
    {
        if(atmCard.Id == null || atmCard.Id == 0)
        {
            _dataContext.ATMCards.Add(atmCard);
        }

        _dataContext.SaveChanges();

        return atmCard;
    }
}