namespace BankApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankApi.Models;
using BankApi.Services;
using BankApi.Operations;
using BankApi.Operations.Customers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly HelloService _helloService;
    private readonly ICustomerService _customerService;

    public CustomersController(ILogger<CustomersController> logger, HelloService helloService, ICustomerService customerService)
    {
        _logger = logger;
        _helloService = helloService;
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult Index(String ?q)
    {
        //HelloService helloService = new HelloService();
        _helloService.Hello();

        _logger.LogInformation("q: " + q);
        List<Customer> customers = _customerService.GetAll();

        _logger.LogInformation("Length of customers: " + customers.Count);

        return Ok(customers);
    }

    [HttpGet("{id}")] // Example: customers/5
    public IActionResult Show(int id)
    {
        Customer customer = CustomerService.Instance.FindById(id);

        Validator validator = new ValidateGetCustomer(customer); 
        validator.run();

        if(validator.HasErrors) {
            return NotFound(validator.Payload);
        } else {
            return Ok(customer);
        }
    }

    // URL: /customers/1/accounts
    // Exercise: Complete the endpoint and test if it works
    [HttpGet("{id}/accounts")]
    public IActionResult Accounts(int id)
    {
        List<Dictionary<string, object>> payload = BankAccountService.Instance.GetAccountsPayload(id);

        return Ok(payload);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Customer customer = CustomerService.Instance.FindById(id);

            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            if(hash["firstName"] != null) {
                customer.FirstName = hash["firstName"].ToString();
            }

            if(hash["lastName"] != null) {
                customer.LastName = hash["lastName"].ToString();
            }

            if(hash["refNumber"] != null) {
                customer.RefNumber = hash["refNumber"].ToString();
            }

            Validator validator = new ValidateSaveCustomer(customer);
            validator.run();

            if(validator.HasErrors) {
                return UnprocessableEntity(validator.Payload);
            } else {
                Customer temp = CustomerService.Instance.Save(customer);
                return Ok(temp);
            }
            
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);

        }
    }

    [HttpPost]
    public IActionResult Create([FromBody]object payload)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            Validator validator = new ValidateSaveCustomer(hash);
            validator.run();

            if(validator.HasErrors) {
                return UnprocessableEntity(validator.Payload);
            } else {
                Customer temp = CustomerService.Instance.Save(hash);
                return Ok(temp);
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
}