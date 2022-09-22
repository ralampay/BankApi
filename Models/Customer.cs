namespace BankApi.Models;

using Microsoft.EntityFrameworkCore;
public class Customer
{
    public Int32 Id { get; set; }
    public string RefNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Customer()
    {

    }

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