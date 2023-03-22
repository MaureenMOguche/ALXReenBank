using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReenBank.Models;

namespace AlxReenBank.Data;

public class AlxReenBankContext : IdentityDbContext<IdentityUser>
{
    public AlxReenBankContext(DbContextOptions<AlxReenBankContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Person>().Property(x => x.DateOfBirth).HasColumnType("date");
    }

    public DbSet<Person> People { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
