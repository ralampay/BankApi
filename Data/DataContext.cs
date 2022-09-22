namespace BankApi.Data;

using Microsoft.EntityFrameworkCore;
using BankApi.Models;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ATMCard> ATMCards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasIndex(c => c.RefNumber)
            .IsUnique();

        builder.Entity<ATMCard>()
            .HasIndex(c => c.PINCode)
            .IsUnique();

        builder.Entity<ATMCard>()
            .HasIndex(c => c.CardNumber)
            .IsUnique();
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}