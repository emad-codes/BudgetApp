using Budget.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BudgetLevel> BudgetLevels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Alias).IsRequired().HasMaxLength(30);
            builder.Entity<User>().HasMany(p => p.BudgetLevels).WithOne(p => p.User).HasForeignKey(p => p.UserId);

            builder.Entity<User>().HasData
            (
                new User { Id = 100, Alias = "EKhan6" }, // Id set manually due to in-memory provider
                new User { Id = 101, Alias = "DDavies" }
            );

            builder.Entity<BudgetLevel>().ToTable("BudgetLevels");
            builder.Entity<BudgetLevel>().HasKey(p => p.Id);
            builder.Entity<BudgetLevel>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<BudgetLevel>().Property(p => p.Level0).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Level1).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Level2).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Level3).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Level4).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Level5).IsRequired().HasMaxLength(50);
            builder.Entity<BudgetLevel>().Property(p => p.Year).IsRequired().HasMaxLength(4);
            builder.Entity<BudgetLevel>().Property(p => p.Status).IsRequired().HasMaxLength(10);
            builder.Entity<BudgetLevel>().Property(p => p.CreatedAt).IsRequired();
            builder.Entity<BudgetLevel>().Property(p => p.ModifiedAt).IsRequired();
        }


    }
}