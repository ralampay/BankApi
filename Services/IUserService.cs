namespace BankApi.Services;

using BankApi.Models;

public interface IUserService 
{
    public User FindByUsername(string username);
    public User Register(string username, string password, string role);
    public User FindByToken(string token);
}