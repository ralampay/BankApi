namespace BankApi.Services;

using BankApi.Models;

public interface ICustomerService
{
    public Customer Save(Customer c);
    public Customer Save(Dictionary<string, object> hash);
    public List<Customer> GetAll();
    public Customer FindById(int id);

    public bool Exists(int id);

    public bool Exists(String refNumber);

    public bool Exists(String refNumber, int notId);

    public Customer FindByRefNumber(string refNumber);
}