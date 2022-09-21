namespace BankApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankApi.Models;
using BankApi.Services;
using BankApi.Operations;
using BankApi.Operations.BankAccounts;
using BankApi.Operations.AccountTransactions;

[ApiController]
[Route("bank_accounts")]
public class BankAccountsController : ControllerBase
{
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        return Ok(BankAccountService.Instance.FindById(id));
    }

    [HttpPost("{id}/transact")]
    public IActionResult Transact(int id, [FromBody]object payload)
    {
        BankAccount bankAccount = BankAccountService.Instance.FindById(id);

        Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

        Validator validator = new ValidateTransact(bankAccount, hash);
        validator.run();

        if(validator.HasErrors) {
            return UnprocessableEntity(validator.Payload);
        } else {
            AccountTransaction accountTransaction = AccountTransactionService.Instance.Transact(
                bankAccount,
                hash["transactionType"].ToString(),
                Decimal.Parse(hash["amount"].ToString())
            );

            return Ok(accountTransaction);
        }
    }

    [HttpGet("{id}/account_transactions")]
    public IActionResult Index(int id)
    {
        List<AccountTransaction> transactions = AccountTransactionService.Instance.GetAllByBankAccount(id);

        List<Dictionary<string, object>> accountTransactions = new List<Dictionary<string, object>>();

        foreach(AccountTransaction t in transactions) {
            BuildHash builder = new BuildHash(t);
            builder.run();

            accountTransactions.Add(builder.Hash);
        }

        return Ok(accountTransactions);
    }
}