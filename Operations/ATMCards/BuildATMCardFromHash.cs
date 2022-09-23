namespace BankApi.Operations.ATMCards;

using BankApi.Models;
using BankApi.Services;

public class BuildATMCardFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public ATMCard ATMCard  {get; set; }

    public ICustomerService _customerService;

    public BuildATMCardFromHash(Dictionary<string, object> hash, ICustomerService customerService)
    {
        Hash = hash;
        ATMCard = new ATMCard();
        _customerService = customerService;
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null) {
            ATMCard.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("pinCode") != null) {
            ATMCard.PINCode = Hash["pinCode"].ToString();
        }

        if(Hash.GetValueOrDefault("cardNumber") != null) {
            ATMCard.CardNumber = Hash["cardNumber"].ToString();
        }

        if(Hash.GetValueOrDefault("customerId") != null) {
            ATMCard.CustomerId = Int32.Parse(Hash["customerId"].ToString());
            //ATMCard.Customer = _customerService.FindById(ATMCard.CustomerId);
        }
    }
}