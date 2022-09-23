namespace BankApi.Models;

public class JointAccount : BankAccount
{
    public Customer SecondaryCustomer { get; set; }

    public JointAccount()
    {

    }

    public Int32 SecondaryCustomerId { get; set; }
    public JointAccount(string accountNumber, Customer customer, Customer secondaryCustomer)
        : base(accountNumber, customer)
    {
        this.SecondaryCustomer = secondaryCustomer;
    }

    public JointAccount(int id, string accountNumber, Customer customer, Customer secondaryCustomer)
        : base(accountNumber, customer)
    {
        this.Id = id;
        this.SecondaryCustomer = secondaryCustomer;
    }

    protected override bool Withdraw(decimal amount)
    {
        if(Balance - amount < 0) {
            return false;
        } else {
            this.Balance = this.Balance - amount;

            return true;
        }
    }
    
    public void Deposit(decimal amount, Customer customer)
    {
        if(customer.RefNumber.Equals(SecondaryCustomer.RefNumber)) {
            base.Deposit(amount);
        }
    }

    public bool Withdraw(decimal amount, string refNumber)
    {
        if(refNumber.Equals(SecondaryCustomer.RefNumber)) {
            return Withdraw(amount);
        }

        return false;
    }
}