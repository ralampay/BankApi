namespace BankApi.Services;

using BankApi.Models;
using BankApi.Exceptions;
using BankApi.Services;
using BankApi.Operations.Users;
using BankApi.Data;

public class AuthenticationService 
{
    private IUserService _userService;
    private DataContext _dataContext;

    public AuthenticationService(IUserService userService, DataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

    public void Logout(User user)
    {
        user.Token = null;

        _dataContext.SaveChanges();
    }

    public string Login(string username, string password)
    {
        User user = _userService.FindByUsername(username);

        if(user == null) {
            throw new InvalidLoginException();
        } else {
            HashPassword hasher = new HashPassword(password);
            hasher.run();

            string encryptedPassword = hasher.Hash;

            if(encryptedPassword.Equals(user.EncryptedPassword)) {
                GenerateToken generator = new GenerateToken(user);
                generator.run();

                user.Token = generator.Token;
                user.LastLogin = DateTime.Now;

                _dataContext.SaveChanges();

                return user.Token;
            } else {
                throw new InvalidLoginException();
            }
        }
    }
}