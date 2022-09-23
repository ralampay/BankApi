namespace BankApi.Services;

using System.Collections.Generic;
using BankApi.Models;
using BankApi.Data;
using BankApi.Operations.ATMCards;

public class EFATMCardService : IATMCardService
{
    private readonly DataContext _dataContext;
    private readonly ICustomerService _customerService;

    public EFATMCardService(DataContext dataContext, ICustomerService customerService)
    {
        _customerService = customerService;
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

    public List<ATMCard> GetAll()
    {
        return _dataContext.ATMCards.ToList();
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

    public ATMCard Save(Dictionary<string, object> hash)
    {
        var builder = new BuildATMCardFromHash(hash, _customerService);
        builder.run();

        return this.Save(builder.ATMCard);
    }
}