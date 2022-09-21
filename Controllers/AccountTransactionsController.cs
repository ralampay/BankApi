namespace BankApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using BankApi.Models;
using BankApi.Services;
using BankApi.Operations;

[ApiController]
[Route("account_transactions")]
public class AccountTransactionsController : ControllerBase
{
    private readonly ILogger<AccountTransactionsController> _logger;

    public AccountTransactionsController(ILogger<AccountTransactionsController> logger)
    {
        _logger = logger;
    }

}