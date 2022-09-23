namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;

using BankApi.Models;

public class RawCustomerService : ICustomerService
{
    private const string CustomersTable = "Customers";

    public Customer Save(Customer c)
    {
        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "INSERT INTO Customers (FirstName, LastName, RefNumber) VALUES (@FirstName, @LastName, @RefNumber)";

        if(c.Id != null) {
            // UPDATE
            sql = "UPDATE Customers SET FirstName=@FirstName, LastName=@LastName, RefNumber=@RefNumber WHERE Id=@Id";
        }

        SqlCommand command = new SqlCommand(sql, connection);

        if(c.Id != null) {
            command.Parameters.AddWithValue("@Id", c.Id);
        }

        command.Parameters.AddWithValue("@FirstName", c.FirstName);
        command.Parameters.AddWithValue("@LastName", c.LastName);
        command.Parameters.AddWithValue("@RefNumber", c.RefNumber);

        command.ExecuteNonQuery();

        connection.Close();

        return c;
    }

    public Customer Save(Dictionary<string, object> hash)
    {
        Int32 id            = Int32.Parse(hash["id"].ToString());
        String firstName    = hash["firstName"].ToString();
        String lastName     = hash["lastName"].ToString();
        String refNumber    = hash["refNumber"].ToString();

        Customer temp = new Customer(id, refNumber, firstName, lastName);

        return this.Save(temp);
    }

    public Customer FindById(int id)
    {
        Customer customer = null;

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT Id, FirstName, LastName, RefNumber FROM " + CustomersTable + " WHERE Id = @id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            customer = new Customer(
                (int)reader["Id"],
                (string)reader["RefNumber"],
                (string)reader["FirstName"],
                (string)reader["LastName"]
            );
        }

        return customer;
    }

    public Customer FindByRefNumber(string refNumber)
    {
        Customer customer = null;

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT Id, FirstName, LastName, RefNumber FROM " + CustomersTable + " WHERE RefNumber = @refNumber";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@refNumber", refNumber);

        //command.Prepare();

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            customer = new Customer(
                (int)reader["Id"],
                (string)reader["RefNumber"],
                (string)reader["FirstName"],
                (string)reader["LastName"]
            );
        }

        connection.Close();

        return customer;
    }

    public List<Customer> GetAll()
    {
        List<Customer> customers = new List<Customer>();

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT Id, FirstName, LastName, RefNumber FROM " + CustomersTable;

        SqlCommand command = new SqlCommand(sql, connection);

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            customers.Add(
                new Customer(
                    (int)reader["Id"],
                    (string)reader["RefNumber"],
                    (string)reader["FirstName"],
                    (string)reader["LastName"]
                )
            );
        }

        connection.Close();

        return customers;
    }

    public bool Exists(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string refNumber)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string refNumber, int notId)
    {
        throw new NotImplementedException();
    }
}