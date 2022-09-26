namespace BankApi.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isAuthenticated = false;
        string validToken = "secret";

        if(context.HttpContext.Request.Headers.ContainsKey("X-AWESOME-AUTHENTICATION")) {

            string tokenValue = context.HttpContext.Request.Headers["X-AWESOME-AUTHENTICATION"].ToString();

            if(tokenValue.Equals(validToken)) {
                isAuthenticated = true;
            }
        }

        if(!isAuthenticated) {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", "Unauthenticated");

            context.Result = new UnauthorizedObjectResult(payload);
        }
    }
}