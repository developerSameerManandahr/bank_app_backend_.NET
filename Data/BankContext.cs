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
            MakePrimaryKeyAutoIncrement(modelBuilder);

            MakeFieldsUnique(modelBuilder);

            ManageRelationShip(modelBuilder);
        }

        private static void ManageRelationShip(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(user => user.AccountDetails);

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.UserDetails)
                .WithOne(details => details.User)
                .HasForeignKey<UserDetails>();

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Transactions);
        }

        private static void MakeFieldsUnique(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasIndex(user => user.UserName)
                .IsUnique();

            modelBuilder
                .Entity<User>()
                .HasIndex(user => user.AccountNumber)
                .IsUnique();

            modelBuilder
                .Entity<AccountDetails>()
                .HasIndex(details => details.UserAccountDetailsId)
                .IsUnique();
        }

        private static void MakePrimaryKeyAutoIncrement(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UserDetails>()
                .Property(u => u.UserUserDetailsId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.UserTransactionId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AccountDetails>()
                .Property(ac => ac.UserAccountDetailsId)
                .ValueGeneratedOnAdd();
        }
    }
}