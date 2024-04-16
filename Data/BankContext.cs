using Microsoft.EntityFrameworkCore;
using SimplifiedBankingApi.Models;

namespace SimplifiedBankingApi.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Id
            modelBuilder.Entity<User>().Property("Id").HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Wallet>().Property("Id").HasDefaultValueSql("newsequentialid()");
            modelBuilder.Entity<Transaction>().Property("Id").HasDefaultValueSql("newsequentialid()");

            // Unique Columns
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Document).IsUnique();


            // Relacionamentos
            modelBuilder.Entity<Wallet>()
                .HasOne(f => f.User)
                .WithMany(u => u.Wallets)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Transaction>()
                .HasOne(f => f.Wallet)
                .WithMany(u => u.Transactions)
                .HasForeignKey(f => f.WalletId);
        }
    }
}
