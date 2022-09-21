namespace BankApi.Models;

public class CheckingAccount : BankAccount
{
    public List<Check> CheckBook { get; private set; }

    public const int NUM_CHECKS = 100;

    public CheckingAccount(string accountNumber, Customer customer)
        : base(accountNumber, customer)
    {
        this.CheckBook = new List<Check>();

        for(int i = 0; i < NUM_CHECKS; i++) {
            string referenceNumber = "CHECK-" + i;
            this.CheckBook.Add(new Check(referenceNumber));
        }
    }

    public CheckingAccount(int id, string accountNumber, Customer customer)
        : base(accountNumber, customer)
    {
        this.Id = id;
        this.CheckBook = new List<Check>();

        for(int i = 0; i < NUM_CHECKS; i++) {
            string referenceNumber = "CHECK-" + i;
            this.CheckBook.Add(new Check(referenceNumber));
        }
    }

    public bool Withdraw(decimal amount, string referenceNumber)
    {
        Check check = CheckBook.Find(c => c.ReferenceNumber.Equals(referenceNumber));

        if(check != null && CheckBook.Count - 1 >= 0) {
            check.Amount = amount;
            // TODO: pop out the check

            return Withdraw(amount);
        }

        return false;
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
}