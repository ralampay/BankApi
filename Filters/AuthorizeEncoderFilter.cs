namespace BankApi.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BankApi.Services;
using BankApi.Models;
using BankApi.Exceptions;

public class AuthorizeEncoderFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Check if the user is valid
        try 
        {
            if(context.HttpContext.Items.ContainsKey("user")) {
                User user = (User)context.HttpContext.Items["user"];

                if(!user.Role.Equals("encoder") && !user.Role.Equals("admin")) {
                    Dictionary<string, object> payload = new Dictionary<string, object>();
                    payload.Add("message", "Only encoder or higher can access this");

                    context.Result = new UnauthorizedObjectResult(payload);
                }
            }
        } 
        catch(Exception e) 
        {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", "Unauthorized");

            context.Result = new UnauthorizedObjectResult(payload);
        }
    }
}