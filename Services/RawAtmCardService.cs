namespace BankApi.Services;

using System.Text.Json;
using System.Data.SqlClient;
using System.Data;
using BankApi.Models;
using BankApi.Services;

public class RawAtmCardService : IATMCardService
{
    private string ATMCardsTable = "ATMCards";

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
}