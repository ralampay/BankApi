namespace BankApi.Services;

using BankApi.Models;

public class MySQLCustomerService : IService, ICustomerService
{
    public Customer Save(Customer c)
    {
        throw new Exception("Not yet implemented");
    }
    public List<Customer> GetAll()
    {
        throw new Exception("Not yet implemented");
    }
    public Customer FindById(int id)
    {
        throw new Exception("Not yet implemented");
    }

    public Customer FindByRefNumber(string refNumber)
    {
        throw new Exception("Not yet implemented");
    }
    public String GetDatabaseProvider()
    {
        return "MySQL";
    }
}