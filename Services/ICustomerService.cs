namespace BankApi.Services;

using BankApi.Models;

public interface ICustomerService
{
    public Customer Save(Customer c);
    public List<Customer> GetAll();
    public Customer FindById(int id);

    public Customer FindByRefNumber(string refNumber);
}