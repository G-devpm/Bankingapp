namespace BankingApp.Data
{
    using BankingApp.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BankDbContext : IdentityDbContext<BankUser, IdentityRole<int>, int>
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasOne(a => a.BankUser)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.BankUserId);
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.FromAccount)
                .WithMany()
                .HasForeignKey(t => t.FromAccountId);
            modelBuilder.Entity<Transfer>()
                .HasOne(t => t.ToAccount)
                .WithMany()
                .HasForeignKey(t => t.ToAccountId);
        }
    }
}
