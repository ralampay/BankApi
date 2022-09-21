namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;
using BankApi.Models;

public class BankAccountService
{
    private string BankAccountsTable = "BankAccounts";

    public BankAccount UpdateBalance(BankAccount bankAccount, Decimal balance)
    {
        bankAccount.Balance += balance;

        Console.WriteLine("bankAccount.Id: " + bankAccount.Id);
        Console.WriteLine("bankAccount.Balance: " + bankAccount.Balance);

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "UPDATE BankAccounts SET Balance=@balance WHERE Id=@id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", bankAccount.Id);
        command.Parameters.AddWithValue("@balance", bankAccount.Balance);

        command.ExecuteNonQuery();

        connection.Close();

        return bankAccount;
    }

    public BankAccount FindById(int id)
    {
        BankAccount bankAccount = null;

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT * FROM " + BankAccountsTable + " WHERE Id = @id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            int bankAccountId       = (int)reader["Id"];
            string type             = (string)reader["AccountType"];
            decimal balance         = (decimal)(double)reader["Balance"];
            string accountNumber    = (string)reader["AccountNumber"];
            int customerId          = (int)reader["CustomerId"];

            Customer customer = CustomerService.Instance.FindById(customerId);
            
            if(type.Equals("ATM")) {
                bankAccount = new ATMAccount(
                    bankAccountId,
                    accountNumber,
                    customer,
                    null
                );
            } else if(type.Equals("JOINT")) {
                int secondaryCustomerId = (int)reader["SecondaryCustomerId"];
                Customer secondaryCustomer = CustomerService.Instance.FindById(secondaryCustomerId);
                bankAccount = new JointAccount(
                    bankAccountId,
                    accountNumber,
                    customer,
                    secondaryCustomer
                );
            } else if(type.Equals("CHECKING")) {
                bankAccount = new CheckingAccount(
                    bankAccountId,
                    accountNumber,
                    customer
                );
            } else {
                throw new Exception("Invalid BankAccount type");
            }

            bankAccount.Balance = balance;
        }

        return bankAccount;
    }

    public List<Dictionary<string, object>> GetAccountsPayload(int customerId)
    {
        List<Dictionary<string, object>> payload = new List<Dictionary<string, object>>();

        List<BankAccount> accounts = GetAccounts(customerId);

        foreach(BankAccount account in accounts)
        {
            Dictionary<string, object> item = new Dictionary<string, object>();
            item["id"]              = account.Id;
            item["accountNumber"]   = account.AccountNumber;
            item["customerId"]      = account.Customer.Id;
            item["balance"]         = account.Balance;

            if(account.GetType() == typeof(ATMAccount)) {
                ATMAccount temp = (ATMAccount)account;
                item["pinCode"] = temp.ATMCard.PINCode;
                item["cardNumber"] = temp.ATMCard.CardNumber;

            } else if(account.GetType() == typeof(JointAccount)) {
                JointAccount temp = (JointAccount)account;
                item["secondaryCustomerId"] = temp.SecondaryCustomer.Id;

            } else if(account.GetType() == typeof(CheckingAccount)) {
                CheckingAccount temp = (CheckingAccount)account;
            }

            payload.Add(item);
        }

        return payload;
    }

    public List<BankAccount> GetAccounts(int customerId)
    {
        // Fetch customer given reference number
        // Instantiate a customer instance to fetch its Id
        Customer c = CustomerService.Instance.FindById(customerId);

        // Use id to make http request in json-server

        List<BankAccount> accounts = new List<BankAccount>();

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT Id,AccountType,AccountNumber,ATMCardId,Balance,SecondaryCustomerId FROM " + BankAccountsTable + " WHERE CustomerId = @customerId";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@customerId", c.Id);

        //command.Prepare();

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            int id                  = (int)reader["Id"];
            string type             = (string)reader["AccountType"];
            decimal balance         = (decimal)(double)reader["Balance"];
            string accountNumber    = (string)reader["AccountNumber"];

            if(type.Equals("ATM")) {
                int atmCardId = (int)reader["ATMCardId"];

                ATMCard card = AtmCardService.Instance.FindById(atmCardId);

                ATMAccount account = new ATMAccount(
                    id,
                    accountNumber,
                    c,
                    card
                );

                account.Balance = balance;

                accounts.Add(account);
            } else if(type.Equals("JOINT")) {
                int secondaryCustomerId = (int)reader["SecondaryCustomerId"];
                Customer secondaryCustomer = CustomerService.Instance.FindById(secondaryCustomerId);

                JointAccount account = new JointAccount(
                    id,
                    accountNumber,
                    c,
                    secondaryCustomer
                );

                account.Balance = balance;

                accounts.Add(account);
            } else if(type.Equals("CHECKING")) {
                CheckingAccount account = new CheckingAccount(
                    id,
                    accountNumber,
                    c
                );

                account.Balance = balance;

                accounts.Add(account);
            }
        }

        connection.Close();

        return accounts;
    }
    public List<BankAccount> GetAccounts(string refNumber)
    {
        // Fetch customer given reference number
        // Instantiate a customer instance to fetch its Id
        Customer c = CustomerService.Instance.FindByRefNumber(refNumber);

        // Use id to make http request in json-server

        List<BankAccount> accounts = new List<BankAccount>();

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT * FROM " + BankAccountsTable + " WHERE CustomerId = @customerId";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@customerId", c.Id);

        //command.Prepare();

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            int id                  = (int)reader["Id"];
            string type             = (string)reader["AccountType"];
            decimal balance         = (decimal)(double)reader["Balance"];
            string accountNumber    = (string)reader["AccountNumber"];

            if(type.Equals("ATM")) {
                int atmCardId = (int)reader["ATMCardId"];

                ATMCard card = AtmCardService.Instance.FindById(atmCardId);

                ATMAccount account = new ATMAccount(
                    accountNumber,
                    c,
                    card
                );

                account.Balance = balance;

                accounts.Add(account);
            } else if(type.Equals("JOINT")) {
                int secondaryCustomerId = (int)reader["SecondaryCustomerId"];
                Customer secondaryCustomer = CustomerService.Instance.FindById(secondaryCustomerId);

                JointAccount account = new JointAccount(
                    accountNumber,
                    c,
                    secondaryCustomer
                );

                account.Balance = balance;

                accounts.Add(account);
            } else if(type.Equals("CHECKING")) {
                CheckingAccount account = new CheckingAccount(
                    accountNumber,
                    c
                );

                account.Balance = balance;

                accounts.Add(account);
            }
        }

        connection.Close();

        return accounts;
    }

    private static BankAccountService instance = null;

    public static BankAccountService Instance {
        get {
            if(instance == null) {
                instance = new BankAccountService();
            }

            return instance;
        }
    }
}