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
    private readonly ICustomerService _customerService;
    private readonly ValidateSaveCustomer _validateSaveCustomer;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService, ValidateSaveCustomer validateSaveCustomer)
    {
        _logger = logger;
        _customerService = customerService;
        _validateSaveCustomer = validateSaveCustomer;
    }

    [HttpGet]
    public IActionResult Index(String ?q)
    {

        _logger.LogInformation("q: " + q);
        List<Customer> customers = _customerService.GetAll();

        _logger.LogInformation("Length of customers: " + customers.Count);

        return Ok(customers);
    }

    [HttpGet("{id}")] // Example: customers/5
    public IActionResult Show(int id)
    {
        Customer customer = _customerService.FindById(id);

        Validator validator = new ValidateGetCustomer(customer); 
        validator.run();

        if(validator.HasErrors) {
            return NotFound(validator.Payload);
        } else {
            return Ok(customer);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Customer customer = _customerService.FindById(id);

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
                Customer temp = _customerService.Save(customer);
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

            _validateSaveCustomer.InitializeParameters(hash);
            _validateSaveCustomer.run();
            //Validator validator = new ValidateSaveCustomer(hash, _customerService);
            //validator.run();

            if(_validateSaveCustomer.HasErrors) {
                return UnprocessableEntity(_validateSaveCustomer.Payload);
            } else {
                Customer temp = _customerService.Save(hash);
                return Ok(temp);
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
}