namespace BankApi.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class CustomerFilter : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("CustomerFilter: Invoking OnActionExecuted()...");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("CustomerFilter: Invoking OnActionExecuting()...");
    }
}