namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;
using BankApi.Models;
using BankApi.Services;
using System.Collections.Generic;

public class RawAtmCardService : IATMCardService
{
    private string ATMCardsTable = "ATMCards";

    public bool Delete(ATMCard atmCard)
    {
        throw new NotImplementedException();
    }

    public List<ATMCard> FindByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }

    public ATMCard FindById(int id)
    {
        ATMCard atmCard = null;

        SqlConnection connection = new SqlConnection(
            ApplicationManager.Instance.GetConnectionString()
        );

        connection.Open();

        String sql = "SELECT * FROM " + ATMCardsTable + " WHERE Id = @id";

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();

        SqlDataReader reader = command.ExecuteReader();

        while(reader.Read()) {
            string cardNumber   = (string)reader["CardNumber"];
            string pinCode      = (string)reader["PinCode"];

            atmCard = new ATMCard(
                (int)reader["Id"],
                pinCode,
                cardNumber
            );
        }

        return atmCard;
    }

    public ATMCard FindByIdAndCustomerId(int id, int customerId)
    {
        throw new NotImplementedException();
    }

    public List<ATMCard> GetAll()
    {
        throw new NotImplementedException();
    }

    public ATMCard Save(ATMCard atmCard)
    {
        throw new NotImplementedException();
    }

    public ATMCard Save(Dictionary<string, object> hash)
    {
        throw new NotImplementedException();
    }
}