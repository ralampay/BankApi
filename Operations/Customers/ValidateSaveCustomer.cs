namespace BankApi.Operations.Customers;

using BankApi.Models;
using BankApi.Services;
using System.Text.Json;

public class ValidateSaveCustomer : Validator
{
    public Int32 Id { get; set; }
    public String FirstName { get; private set; }
    public String LastName { get; private set; }
    public String RefNumber { get; private set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null) {
            this.Id = Int32.Parse(hash["id"].ToString());
        }

        if(hash.GetValueOrDefault("firstName") != null) {
            this.FirstName = hash["firstName"].ToString();
        }

        if(hash.GetValueOrDefault("lastName") != null) {
            this.LastName = hash["lastName"].ToString();
        }

        if(hash.GetValueOrDefault("refNumber") != null) {
            this.RefNumber  = hash["refNumber"].ToString();
        }
    }

    private readonly ICustomerService _customerService;

    public ValidateSaveCustomer(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public ValidateSaveCustomer(Dictionary<string, object> hash, ICustomerService customerService)
    {
        _customerService = customerService;

        if(hash.GetValueOrDefault("id") != null) {
            this.Id = JsonSerializer.Deserialize<int>((JsonElement)hash["id"]);
        }

        if(hash.GetValueOrDefault("firstName") != null) {
            this.FirstName = hash["firstName"].ToString();
        }

        if(hash.GetValueOrDefault("lastName") != null) {
            this.LastName = hash["lastName"].ToString();
        }

        if(hash.GetValueOrDefault("refNumber") != null) {
            this.RefNumber  = hash["refNumber"].ToString();
        }
    }

    public override void run()
    {
        Console.WriteLine("Customer ID: " + this.Id);
        Console.WriteLine("FirstName: " + this.FirstName);
        Console.WriteLine("LastName: " + this.LastName);
        Console.WriteLine("RefNumber: " + this.RefNumber);

        if(this.FirstName == null || this.FirstName.Equals("")) {
            String msg = "First name is required";
            this.AddError(msg, "firstName");
        }

        if(this.LastName == null || this.LastName.Equals("")) {
            String msg = "Last name is required";
            this.AddError(msg, "lastName");
        }

        if(this.RefNumber == null || this.RefNumber.Equals("")) {
            String msg = "Ref number is required";
            this.AddError(msg, "refNumber");
        } else if(this.Id == null || this.Id == 0) {
            if(_customerService.Exists(this.RefNumber)) {
                String msg = "Ref number is already taken";
                this.AddError(msg, "refNumber");
            }
        } else {
            // Validating refNumber for update
            if(!_customerService.Exists(this.Id)) {
                String msg = "Customer not found";
                this.AddError(msg, "id");
            } else {
                if(_customerService.Exists(this.RefNumber, this.Id)) {
                    String msg = "Ref number is already taken";
                    this.AddError(msg, "refNumber");
                }
            }
        }
    }
}