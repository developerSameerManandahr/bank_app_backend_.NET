using Microsoft.EntityFrameworkCore;
using worksheet2.Model;

namespace worksheet2.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AccountDetails> AccountDetails { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder
        )
        {
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.AccountDetails)
                .WithOne(details => details.User);
            
            modelBuilder
                .Entity<User>()
                .HasOne(u => u.UserDetails)
                .WithOne(details => details.User);
            
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(details => details.User);
            modelBuilder.Entity<User>()
                .ToTable(nameof(Users));
            modelBuilder.Entity<UserDetails>()
                .ToTable(nameof(UserDetails));
            modelBuilder.Entity<Transaction>()
                .ToTable(nameof(Transactions))
                .HasOne<User>();
            modelBuilder.Entity<AccountDetails>()
                .ToTable(nameof(AccountDetails))
                .HasOne<User>();
        }
    }
}