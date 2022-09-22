namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;

using BankApi.Models;

public class RawAccountTransactionService : IAccountTransactionService
{

    private readonly IBankAccountService _bankAccountService;

    public RawAccountTransactionService(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public AccountTransaction Transact(BankAccount bankAccount, String transactionType, Decimal amount)
    {
        if(transactionType == "DEPOSIT") {
            return this.Deposit(bankAccount, amount);
        } else if(transactionType == "WITHDRAW") {
            return this.Withdraw(bankAccount, amount);
        } else {
            throw new Exception("Invalid transactionType");
        }
    }

    private AccountTransaction Deposit(BankAccount bankAccount, Decimal amount)
    {
        Decimal startingBalance = bankAccount.Balance;
        Decimal endingBalance = bankAccount.Balance + amount;

        if(endingBalance < 0) {
            throw new Exception("Invalid amount: " + amount);
        }

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        AccountTransaction accountTransaction = new AccountTransaction {
            BankAccount = bankAccount,
            Amount = amount,
            TransactionType = "DEPOSIT",
            StartingBalance = startingBalance,
            EndingBalance = endingBalance,
            TransactedAt = DateTime.Now
        };

        connection.Open();

        String sql = "INSERT INTO AccountTransactions (TransactionType, Amount, BankAccountId, TransactedAt, StartingBalance, EndingBalance) ";
        sql += "VALUES (@TransactionType, @Amount, @BankAccountId, @TransactedAt, @StartingBalance, @EndingBalance)";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@TransactionType", accountTransaction.TransactionType);
        command.Parameters.AddWithValue("@Amount", accountTransaction.Amount);
        command.Parameters.AddWithValue("@BankAccountId", bankAccount.Id);
        command.Parameters.AddWithValue("@TransactedAt", accountTransaction.TransactedAt);
        command.Parameters.AddWithValue("@StartingBalance", accountTransaction.StartingBalance);
        command.Parameters.AddWithValue("@EndingBalance", accountTransaction.EndingBalance);

        command.ExecuteNonQuery();

        connection.Close();

        _bankAccountService.UpdateBalance(bankAccount, amount);

        return accountTransaction;
    }

    private AccountTransaction Withdraw(BankAccount bankAccount, Decimal amount)
    {
        return null;
    }

    public List<AccountTransaction> GetAllByBankAccount(int id)
    {
        BankAccount o = _bankAccountService.FindById(id);

        // Use id to make http request in json-server

        List<AccountTransaction> accountTransactions = new List<AccountTransaction>();

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT * FROM AccountTransactions WHERE BankAccountId = @bankAccountId";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@bankAccountId", id);

        //command.Prepare();

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            int transactionId       = (int)reader["Id"];
            string transactionType  = (string)reader["TransactionType"];
            decimal amount          = (decimal)(double)reader["Amount"];
            DateTime transactedAt   = (DateTime)reader["TransactedAt"];
            BankAccount bankAccount = _bankAccountService.FindById((int)reader["BankAccountId"]);
            decimal startingBalance = (decimal)(double)reader["StartingBalance"];
            decimal endingBalance   = (decimal)(double)reader["EndingBalance"];

            AccountTransaction t = new AccountTransaction {
                Id = transactionId,
                TransactionType = transactionType,
                Amount = amount,
                TransactedAt = transactedAt,
                BankAccount = bankAccount,
                StartingBalance = startingBalance,
                EndingBalance = endingBalance
            };

            accountTransactions.Add(t);
        }

        connection.Close();

        return accountTransactions;
    }
}