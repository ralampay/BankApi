namespace BankApi.Operations.BankAccounts;

using BankApi.Models;
using BankApi.Services;
using System.Text.Json;

using System.Linq;

public class ValidateTransact : Validator
{
    public BankAccount BankAccount { get; private set; }
    public String TransactionType {get; private set; }
    public Decimal Amount { get; private set; }

    public ValidateTransact(BankAccount bankAccount, Dictionary<string, object> hash)
    {
        this.BankAccount = bankAccount;

        if(hash["transactionType"] != null) {
            TransactionType = hash["transactionType"].ToString();
        }

        if(hash["amount"] != null) {
            Amount = Decimal.Parse(hash["amount"].ToString());
        }
    }

    public override void run()
    {
        if(this.BankAccount == null) {
            String msg = "Bank account not found";
            this.AddError(msg, "bankAccount");
        }

        String[] validTransactions = { "DEPOSIT", "WITHDRAW" };

        if(this.TransactionType == null) {
            String msg = "Transaction type required";
            this.AddError(msg, "transactionType");
        } else {
            bool isValidTransactionType = false;
            foreach(String type in validTransactions) {
                if(type.Equals(this.TransactionType)) {
                    isValidTransactionType = true;
                    break;
                }
            }

            if(!isValidTransactionType) {
                String msg = "Invalid transaction type";
                this.AddError(msg, "transactionType");
            } else if(this.TransactionType.Equals("WITHDRAW") && this.BankAccount != null && this.Amount != null) {
                if(this.BankAccount.Balance - this.Amount < 0) {
                    String msg = "Invalid amount";
                    this.AddError(msg, "amount");
                }
            }
        }

        if(this.Amount == null) {
            String msg = "Amount is required";
            this.AddError(msg, "amount");
        } else if(this.Amount <= 0) {
            String msg = "Amount should be positive";
            this.AddError(msg, "amount");
        }
    }
}