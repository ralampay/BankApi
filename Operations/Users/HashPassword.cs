using System.Security.Cryptography;
using System.Text;

namespace BankApi.Operations.Users;

public class HashPassword
{
    public string InputString { get; set; }
    public string Hash { get; set; }

    public HashPassword(string inputString)
    {
        InputString = inputString;
    }

    public void run()
    {
        SHA256 sha256 = SHA256.Create();
        byte[] sourceBytes = Encoding.UTF8.GetBytes(InputString);
        byte[] hashBytes = sha256.ComputeHash(sourceBytes);

        Hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
    }
}