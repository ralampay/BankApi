namespace BankApi.Operations.Customers;

using BankApi.Models;
using BankApi.Operations;

public class ValidateGetCustomer : Validator
{
    public Customer Customer { get; private set; }
    public ValidateGetCustomer(Customer c)
    {
        Customer = c;
    }

    public override void run()
    {
        if(Customer == null)
        {
            string m = "Customer not found";
            Messages.Add(m);

            Dictionary<string, object> mHash = new Dictionary<string, object>();
            mHash["customer"] = m;

            MessageHashes.Add(mHash);
        }
    }
}