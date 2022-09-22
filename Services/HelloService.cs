namespace BankApi.Services;

public class HelloService
{
    private ICustomerService _customerService;
    private IBankAccountService _bankAccountService;

    public HelloService(ICustomerService customerService, IBankAccountService bankAccountService)
    {
        _customerService = customerService;
        _bankAccountService = bankAccountService;
    }

    public void Hello()
    {
        _customerService.GetAll();
        _bankAccountService.GetAll();
    }
}