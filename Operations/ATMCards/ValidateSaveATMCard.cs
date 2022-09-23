namespace BankApi.Operations.ATMCards;

using BankApi.Models;
using BankApi.Services;
using System.Text.Json;

public class ValidateSaveATMCard : Validator
{
    public Int32 Id { get; set; }
    public String PINCode { get; private set; }
    public String CardNumber { get; private set; }
    public Int32 CustomerId { get; private set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = Int32.Parse(hash["id"].ToString());
        }

        if(hash.GetValueOrDefault("pinCode") != null) {
            this.PINCode = hash["pinCode"].ToString();
        }

        if(hash.GetValueOrDefault("cardNumber") != null) {
            this.CardNumber = hash["cardNumber"].ToString();
        }

        if(hash.GetValueOrDefault("customerId") != null) {
            this.CustomerId  = Int32.Parse(hash["customerId"].ToString());
        }
    }

    private readonly IATMCardService _atmCardService;
    private readonly ICustomerService _customerService;

    public ValidateSaveATMCard(IATMCardService atmCardService, ICustomerService customerService)
    {
        _customerService = customerService;
        _atmCardService = atmCardService;
    }

    public ValidateSaveATMCard(Dictionary<string, object> hash, IATMCardService atmCardService, ICustomerService customerService)
    {
        _customerService = customerService;
        _atmCardService = atmCardService;
        this.InitializeParameters(hash);
    }

    public override void run()
    {
        if(this.CardNumber == null || this.CardNumber.Equals("")) {
            String msg = "Card number is required";
            this.AddError(msg, "cardNumber");
        }

        if(this.PINCode == null || this.PINCode.Equals("")) {
            String msg = "PINCode is required";
            this.AddError(msg, "pinCode");
        }

        if(this.CustomerId == null || this.CustomerId == 0) {
            String msg = "Customer is required";
            this.AddError(msg, "customerId");
        } else if(!_customerService.Exists(this.CustomerId)) {
            String msg = "Customer not found";
            this.AddError(msg, "customerId");
        }
    }
}