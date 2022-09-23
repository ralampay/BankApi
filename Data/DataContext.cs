namespace BankApi.Data;

using Microsoft.EntityFrameworkCore;
using BankApi.Models;

public class DataContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ATMCard> ATMCards { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ATMAccount>()
            .HasDiscriminator(bankAccount => bankAccount.AccountType)
            .HasValue("ATM");

        /*
        builder.Entity<JointAccount>()
            .HasDiscriminator(bankAccount => bankAccount.AccountType)
            .HasValue("JOINT");
        */

        builder.Entity<Customer>()
            .HasIndex(c => c.RefNumber)
            .IsUnique();

        builder.Entity<ATMCard>()
            .HasIndex(c => c.PINCode)
            .IsUnique();

        builder.Entity<ATMCard>()
            .HasIndex(c => c.CardNumber)
            .IsUnique();

        builder.Entity<Customer>()
            .HasMany(c => c.ATMCards);
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {

    }
}