using BankApi.Models;
using System.Data.SqlClient;

public class ApplicationManager
{
    public String GetConnectionString()
    {
        String connectionString = "";

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = "172.18.0.1";
        builder.UserID = "SA";
        builder.Password = "Developer-Password";
        builder.InitialCatalog = "Bank";

        connectionString = builder.ConnectionString;

        return connectionString;
    }

    private static ApplicationManager instance = null;

    public static ApplicationManager Instance {
        get {
            if(instance == null) {
                instance = new ApplicationManager();
            }

            return instance;
        }
    }
}