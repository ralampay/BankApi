namespace BankApi.Models;

public class ATMCard
{
    public Int32 Id { get; set; }
    public String PINCode { get; set; }

    public String CardNumber { get; set; }

    public Int32 CustomerId { get; set;}

    public Customer Customer { get; set; }

    public ATMCard() {} 

    public ATMCard(String pinCode, String cardNumber) 
    {
        this.PINCode = pinCode;
        this.CardNumber = cardNumber;
    }

    public ATMCard(Int32 id, String pinCode, String cardNumber)
        : this(pinCode, cardNumber)
    {
        this.Id = id;
    }
}