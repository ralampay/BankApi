namespace BankApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankApi.Models;
using BankApi.Services;
using BankApi.Operations;
using BankApi.Operations.Customers;
using BankApi.Filters;

[ApiController]
[Route("customers")]
[ServiceFilter(typeof(AuthenticationFilter))]
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerService _customerService;
    private readonly IATMCardService _atmCardService;
    private readonly ValidateSaveCustomer _validateSaveCustomer;

    public CustomersController(
        ILogger<CustomersController> logger, 
        ICustomerService customerService,
        IATMCardService atmCardService,
        ValidateSaveCustomer validateSaveCustomer
    )
    {
        _logger = logger;
        _customerService = customerService;
        _atmCardService = atmCardService;
        _validateSaveCustomer = validateSaveCustomer;
    }

    /**
    1. Return atm cards in the following format:
    {
        id:,
        cardNumber:,
        customerRefNumber
    }
    **/ 
    [HttpGet("{id}/atm_cards")]
    public IActionResult AtmCards(int id)
    {
        Customer customer = _customerService.FindById(id);

        // Eager loading

        List<ATMCard> cards = customer.ATMCards.ToList();

        List<Dictionary<string, object>> items = new List<Dictionary<string, object>>();

        foreach(ATMCard card in cards)
        {
            Dictionary<string, object> item = new Dictionary<string, object>();
            item["id"] = card.Id;
            item["cardNumber"] = card.CardNumber;
            item["customerReferenceNumber"] = customer.RefNumber;

            items.Add(item);
        }

        return Ok(items);
    }

    [HttpGet]
    [ServiceFilter(typeof(CustomerFilter))]
    public IActionResult Index(String ?q)
    {
        List<Customer> customers = _customerService.GetAll();

        Console.WriteLine("Returning all customers...");
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
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            hash["id"] = id;
            _validateSaveCustomer.InitializeParameters(hash);
            _validateSaveCustomer.run();

            if(_validateSaveCustomer.HasErrors) {
                return UnprocessableEntity(_validateSaveCustomer.Payload);
            } else {
                return Ok(_customerService.Save(hash));
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            _logger.LogInformation(e.Message);
            _logger.LogInformation(e.StackTrace);
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