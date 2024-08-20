using KrispyKreme.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KrispyKreme.Data.Persistance
{
    public class KrispyDbContext : DbContext
    {
        public KrispyDbContext(DbContextOptions<KrispyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Doughnut> Doughnuts { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sale to Customer relationship
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            // Sale to Doughnut relationship
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Doughnut)
                .WithMany(d => d.Sales)
                .HasForeignKey(s => s.DoughnutId);

            // Seed initial doughnut catalog
            modelBuilder.Entity<Doughnut>().HasData(
                new Doughnut { Id = 1, Name = "Original Glazed Doughnut" },
                new Doughnut { Id = 2, Name = "Chocolate Iced Glazed Doughnut" },
                new Doughnut { Id = 3, Name = "Glazed Raspberry Filled Doughnut" },
                new Doughnut { Id = 4, Name = "Glazed Lemon Filled Doughnut" },
                new Doughnut { Id = 5, Name = "Pumpkin Spice Cake Doughnut" }
            );
        }
    }
}
