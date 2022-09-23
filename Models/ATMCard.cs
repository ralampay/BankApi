namespace BankApi.Models;

public class ATMCard
{
    public Int32 Id { get; set; }
    public string PINCode { get; set; }
    public string CardNumber { get; set; }
    public Customer Customer { get; set; }
    public Int32 CustomerId { get; set; }

    public ATMCard() 
    {

    }

    public ATMCard(int id, string pinCode, string cardNumber)
    {
        this.Id = id;
        this.PINCode = pinCode;
        this.CardNumber = cardNumber;
    }

    public ATMCard(string pinCode, string cardNumber)
    {
        this.PINCode = pinCode;
        this.CardNumber = cardNumber;
    }
}