namespace BankApi.Models;

public class Customer
{
    public int Id { get; private set; }
    public string RefNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public Customer(string refNumber, string firstName, string lastName)
    {
        this.RefNumber = refNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public Customer(int id, string refNumber, string firstName, string lastName)
    {
        this.Id = id;
        this.RefNumber = refNumber;
        this.FirstName = firstName;
        this.LastName = lastName;
    }

    public string FullName()
    {
        return this.LastName + ", " + this.FirstName + "(" + this.RefNumber + ")";
    }
}