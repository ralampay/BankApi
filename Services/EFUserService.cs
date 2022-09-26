namespace BankApi.Services;

using BankApi.Models;
using BankApi.Data;
using System.Security.Cryptography;
using System.Text;
using BankApi.Operations.Users;

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
        HashPassword hasher = new HashPassword(password);
        hasher.run();

        string encryptedPassword = hasher.Hash;

        User user = new User() {
            Username = username,
            EncryptedPassword = encryptedPassword
        };

        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();

        return user;
    }
}