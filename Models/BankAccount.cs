namespace BankApi.Models;

public abstract class BankAccount
{
    // protected: Base class and all inheriting classes can use protected values
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string AccountNumber { get; private set; }
    public Customer Customer { get; private set; }

    public BankAccount(string accountNumber, Customer customer)
    {
        this.AccountNumber = accountNumber;
        this.Customer = customer;
    }

    public void Deposit(decimal amount)
    {
        this.Balance = this.Balance + amount;
    }

    public string ToString()
    {
        string output = "";

        return output;
    }

    protected abstract bool Withdraw(decimal amount);
}