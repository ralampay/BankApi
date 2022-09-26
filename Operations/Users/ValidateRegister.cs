namespace BankApi.Operations.Users;

using BankApi.Models;
using BankApi.Services;

public class ValidateRegister : Validator
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }
    public string Role { get; set; }

    private readonly IUserService _userService;

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("username") != null) {
            this.Username = hash["username"].ToString();
        }

        if(hash.GetValueOrDefault("password") != null) {
            this.Password = hash["password"].ToString();
        }

        if(hash.GetValueOrDefault("passwordConfirmation") != null) {
            this.PasswordConfirmation = hash["passwordConfirmation"].ToString();
        }

        if(hash.GetValueOrDefault("role") != null) {
            this.Role = hash["role"].ToString();
        }
    }

    public ValidateRegister(IUserService userService)
    {
        _userService = userService;
    }

    public override void run()
    {
        if(this.Password == null || this.Password.Equals("")) {
            String msg = "Password is required";
            this.AddError(msg, "password");
        }

        if(this.PasswordConfirmation == null || this.PasswordConfirmation.Equals("")) {
            String msg = "PasswordConfirmation is required";
            this.AddError(msg, "passwordConfirmation");
        }

        if(Password != null && PasswordConfirmation != null && !Password.Equals(PasswordConfirmation))
        {
            String msg = "Passwords have to be equal";
            this.AddError(msg, "password");
        }

        if(this.Username == null || this.Username.Equals("")) {
            String msg = "Username is required";
            this.AddError(msg, "username");
        }

        if(this.Role == null || this.Role.Equals("")) {
            String msg = "Role is required";
            this.AddError(msg, "role");
        }
    }
}