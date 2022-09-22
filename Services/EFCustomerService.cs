using BankApi.Models;

namespace BankApi.Services;

public class EFCustomerService : ICustomerService
{
    public Customer FindById(int id)
    {
        throw new NotImplementedException();
    }

    public Customer FindByRefNumber(string refNumber)
    {
        throw new NotImplementedException();
    }

    public List<Customer> GetAll()
    {
        throw new NotImplementedException();
    }

    public Customer Save(Customer c)
    {
        throw new NotImplementedException();
    }

    public Customer Save(Dictionary<string, object> hash)
    {
        throw new NotImplementedException();
    }
}