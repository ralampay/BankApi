namespace BankApi.Models;

public class Check
{
    public decimal Amount { get; set; }
    public string ReferenceNumber { get; private set; }

    public Check(string referenceNumber)
    {
        // Note: Default to 0 for Amount

        this.ReferenceNumber = referenceNumber;
    }
}