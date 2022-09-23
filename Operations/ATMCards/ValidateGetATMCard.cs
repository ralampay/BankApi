namespace BankApi.Operations.ATMCards;

using BankApi.Models;
using BankApi.Services;
using System.Text.Json;

public class ValidateGetATMCard : Validator
{
    public ATMCard ATMCard { get; set; }

    public ValidateGetATMCard(ATMCard atmCard)
    {
        ATMCard = atmCard;
    }
    public override void run()
    {
        if(this.ATMCard == null) {
            String msg = "ATM Card not found";
            this.AddError(msg, "id");
        }
    }
}