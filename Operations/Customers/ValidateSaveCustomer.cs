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

    public ValidateSaveCustomer(Customer customer)
    {
        this.Id         = customer.Id;
        this.FirstName  = customer.FirstName;
        this.LastName   = customer.LastName;
        this.RefNumber  = customer.RefNumber;
    }

    public override void run()
    {
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
        } else if(this.Id == null) {
            Customer temp = _customerService.FindByRefNumber(this.RefNumber);

            if(temp != null) {
                String msg = "Ref number is already taken";
                this.AddError(msg, "refNumber");
            }
        } else {
            // Validating refNumber for update
            Customer temp = _customerService.FindById(this.Id);

            if(temp == null) {
                String msg = "Customer not found";
                this.AddError(msg, "id");
            } else {
                if(!this.RefNumber.Equals(temp.RefNumber)) {
                    // CHeck for uniqueness
                    Customer existingCustomer = _customerService.FindByRefNumber(this.RefNumber);

                    if(existingCustomer != null) {
                        String msg = "Ref number is already taken";
                        this.AddError(msg, "refNumber");
                    }
                }
            }
        }
    }
}