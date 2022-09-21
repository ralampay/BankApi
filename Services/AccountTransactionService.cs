namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;

using BankApi.Models;

public class AccountTransactionService
{
    public List<AccountTransaction> GetAllByBankAccount(int id)
    {
        BankAccount o = BankAccountService.Instance.FindById(id);

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
            BankAccount bankAccount = BankAccountService.Instance.FindById((int)reader["BankAccountId"]);
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

    private static AccountTransactionService instance = null;

    public static AccountTransactionService Instance {
        get {
            if(instance == null) {
                instance = new AccountTransactionService();
            }

            return instance;
        }
    }
}