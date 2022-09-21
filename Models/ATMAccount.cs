namespace BankApi.Models;

public class ATMAccount : BankAccount
{
    public ATMCard ATMCard { get; private set; }

    public ATMAccount(string accountNumber, Customer customer, string pinCode, string cardNumber)
        : base(accountNumber, customer)
    {
        this.ATMCard = new ATMCard(pinCode, cardNumber);
    }

    public ATMAccount(string accountNumber, Customer customer, ATMCard atmCard)
        : base(accountNumber, customer)
    {
        this.ATMCard = atmCard;
    }

    public ATMAccount(int id, string accountNumber, Customer customer, ATMCard atmCard)
        : base(accountNumber, customer)
    {
        this.Id = id;
        this.ATMCard = atmCard;
    }

    public void Deposit(decimal amount, string pinCode)
    {
        if(pinCode.Equals(this.ATMCard.PINCode)) {
            Deposit(amount);
        }
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

    public bool Withdraw(decimal amount, string pinCode)
    {
        if(pinCode.Equals(ATMCard.PINCode)) {
            return Withdraw(amount);
        }

        return false;
    }
}