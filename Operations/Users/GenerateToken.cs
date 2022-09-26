namespace BankApi.Operations.Users;

using BankApi.Models;
using System.Diagnostics;

class GenerateToken
{
    public string Token { get; private set; }

    private User _user;

    public GenerateToken(User user)
    {
        _user = user;
    }

    public void run()
    {
        Token = (Guid.NewGuid()).ToString();
    }
}