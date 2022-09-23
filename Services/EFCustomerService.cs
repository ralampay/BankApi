using BankApi.Models;
using BankApi.Data;
using BankApi.Operations.Customers;
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

    public bool Exists(int id)
    {
        return _dataContext.Customers.SingleOrDefault(c => c.Id == id) != null;
    }

    public bool Exists(string refNumber)
    {
        return _dataContext.Customers.SingleOrDefault(c => c.RefNumber.Equals(refNumber)) != null;
    }

    // Exists where refNumber is existing for all other customers that are not based on id
    public bool Exists(string refNumber, int notId)
    {
        return _dataContext.Customers.SingleOrDefault(c => c.Id != notId && c.RefNumber.Equals(refNumber)) != null;
    }

    public Customer FindById(int id)
    {
        Customer temp = _dataContext.Customers.SingleOrDefault(c => c.Id == id);

        return temp;
    }

    public Customer FindByRefNumber(string refNumber)
    {
        Customer c = _dataContext.Customers
                        .SingleOrDefault(c => c.RefNumber == refNumber);

        return c;
        /*
        if(refNumber != null) {
            var pRefNumber = new SqlParameter(
                "refNumber",
                refNumber
            );

            Customer c = _dataContext.Customers
                            .FromSqlRaw("SELECT * FROM dbo.Customers WHERE RefNumber=@refNumber", pRefNumber)
                            .SingleOrDefault();

            return c;
        }

        return null;
        */
    }

    public List<Customer> GetAll()
    {
        List<Customer> customers = _dataContext.Customers.ToList();

        return customers;
    }

    public Customer Save(Customer c)
    {
        if(c.Id == null || c.Id == 0) {
            _dataContext.Customers.Add(c);
        } else {
            Customer temp = this.FindById(c.Id);
            temp.FirstName = c.FirstName;
            temp.LastName = c.LastName;
            temp.RefNumber = c.RefNumber;
        }
        
        _dataContext.SaveChanges();

        return c;
    }

    public Customer Save(Dictionary<string, object> hash)
    {
        var builder = new BuildCustomerFromHash(hash);
        builder.run();

        return Save(builder.Customer);
    }
}