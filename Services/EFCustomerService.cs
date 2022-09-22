using BankApi.Models;
using BankApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
namespace BankApi.Services;

public class EFCustomerService : ICustomerService
{
    private readonly DataContext _dataContext;

    public EFCustomerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Customer FindById(int id)
    {
        Customer c = _dataContext.Customers.SingleOrDefault(c => c.Id == id);

        return c;
    }

    public Customer FindByRefNumber(string refNumber)
    {
        /*
        Customer c = _dataContext.Customers
                        .SingleOrDefault(c => c.RefNumber == refNumber);

        return c;
        */
        var pRefNumber = new SqlParameter(
            "refNumber",
            refNumber
        );

        Customer c = _dataContext.Customers
                        .FromSqlRaw("SELECT * FROM dbo.Customers WHERE RefNumber=@refNumber", pRefNumber)
                        .SingleOrDefault();

        return c;
    }

    public List<Customer> GetAll()
    {
        List<Customer> customers = _dataContext.Customers.ToList();

        return customers;
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