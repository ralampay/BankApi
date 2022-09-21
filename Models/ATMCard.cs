namespace BankApi.Models;

public class ATMCard
{
    public Int32 Id { get; set; }
    public string PINCode { get; private set; }
    public string CardNumber { get; private set; }

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