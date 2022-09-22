using BankApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HelloWorld helloWorld = new HelloWorld();
builder.Services.AddScoped<IBankAccountService, OracleBankAccountService>();
builder.Services.AddScoped<ICustomerService>(provider => {
    return new MySQLCustomerService();
});
builder.Services.AddScoped<HelloService, HelloService>();

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
