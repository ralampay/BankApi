namespace BankApi.Controllers;

using BankApi.Models;
using BankApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankApi.Operations.Users;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ValidateRegister _validateRegister;

    public UsersController(IUserService userService, ValidateRegister validateRegister)
    {
        _userService = userService;
        _validateRegister = validateRegister;
    }

    [HttpPost("register")] // POST /users/register
    public ActionResult Register([FromBody]object payload)
    {
        try
        {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateRegister.InitializeParameters(hash);
            _validateRegister.run();

            if(_validateRegister.HasErrors) {
                return UnprocessableEntity(_validateRegister.Payload);
            } else {
                return Ok(_userService.Register(hash["username"].ToString(), hash["password"].ToString()));
            }
        }
        catch(Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
}