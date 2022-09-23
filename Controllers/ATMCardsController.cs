namespace BankApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BankApi.Models;
using BankApi.Services;
using BankApi.Operations;
using BankApi.Operations.ATMCards;

/**
Complete ATMCardsController by supplying the implementation of the missing classes
**/
[ApiController]
[Route("atm_cards")]
public class ATMCardsController : ControllerBase
{
    private readonly ILogger<ATMCardsController> _logger;
    private readonly IATMCardService _atmCardService;
    private readonly ValidateSaveATMCard _validateSaveATMCard;

    public ATMCardsController(ILogger<ATMCardsController> logger, IATMCardService atmCardService, ValidateSaveATMCard validateSaveATMCard)
    {
        _logger = logger;
        _atmCardService = atmCardService;
        _validateSaveATMCard = validateSaveATMCard;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<ATMCard> atmCards = _atmCardService.GetAll();

        return Ok(atmCards);
    }

    [HttpGet("{id}/customer")]
    public IActionResult GetCustomer(int id)
    {
        ATMCard atmCard =_atmCardService.FindById(id);

        return Ok(atmCard.Customer);
    }

    [HttpGet("{id}")] // Example: atm_cards/5
    public IActionResult Show(int id)
    {
        ATMCard atmCard = _atmCardService.FindById(id);

        Validator validator = new ValidateGetATMCard(atmCard); 
        validator.run();

        if(validator.HasErrors) {
            return NotFound(validator.Payload);
        } else {
            return Ok(atmCard);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            hash["id"] = id;
            _validateSaveATMCard.InitializeParameters(hash);
            _validateSaveATMCard.run();

            if(_validateSaveATMCard.HasErrors) {
                return UnprocessableEntity(_validateSaveATMCard.Payload);
            } else {
                return Ok(_atmCardService.Save(hash));
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

            _validateSaveATMCard.InitializeParameters(hash);
            _validateSaveATMCard.run();
            //Validator validator = new ValidateSaveATMCard(hash, _atmCardService);
            //validator.run();

            if(_validateSaveATMCard.HasErrors) {
                return UnprocessableEntity(_validateSaveATMCard.Payload);
            } else {
                ATMCard temp = _atmCardService.Save(hash);

                hash["id"] = temp.Id;
                return Ok(hash);
            }
        } catch(Exception e) {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }
}