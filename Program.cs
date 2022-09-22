using Microsoft.EntityFrameworkCore;
using BankApi.Services;
using BankApi.Operations.Customers;
using BankApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSqlConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountTransactionService, RawAccountTransactionService>();
builder.Services.AddScoped<IATMCardService, RawAtmCardService>();
builder.Services.AddScoped<IBankAccountService, RawBankAccountService>();
builder.Services.AddScoped<ICustomerService, RawCustomerService>();
builder.Services.AddScoped<ValidateSaveCustomer, ValidateSaveCustomer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
