namespace BankApi.Models;

public class User
{
    public Int32 Id { get; set; }
    public string Username { get; set; }
    public string EncryptedPassword { get; set; }
    public string ?Token { get; set; }
    public DateTime ?LastLogin { get; set; }
}