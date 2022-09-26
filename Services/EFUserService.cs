namespace BankApi.Services;

using BankApi.Models;
using BankApi.Data;
using System.Security.Cryptography;
using System.Text;

public class EFUserService : IUserService
{
    private readonly DataContext _dataContext;

    public EFUserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public User FindByUsername(string username)
    {
        return _dataContext.Users.SingleOrDefault(u => u.Username.Equals(username));
    }

    public User Register(string username, string password)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = sha256.ComputeHash(sourceBytes);

        string encryptedPassword = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

        User user = new User() {
            Username = username,
            EncryptedPassword = encryptedPassword
        };

        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();

        return user;
    }
}