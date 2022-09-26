namespace BankApi.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BankApi.Services;

public class CustomerFilter : Attribute, IActionFilter
{
    private readonly ICustomerService _customerService;

    public CustomerFilter(ICustomerService customerService) 
    {
        _customerService = customerService;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("CustomerFilter: Invoking OnActionExecuted()...");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Int32 customerCount = _customerService.GetAll().Count;
        Console.WriteLine("Number of customers in database: " + customerCount);
        Console.WriteLine("CustomerFilter: Invoking OnActionExecuting()...");
    }
}