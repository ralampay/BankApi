namespace BankApi.Data;

using Microsoft.EntityFrameworkCore;
using BankApi.Models;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}