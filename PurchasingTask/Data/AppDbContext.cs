using Microsoft.EntityFrameworkCore;
using PurchasingTask.Models;

namespace PurchasingTask.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Item> Items { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrderItem>()
						.HasKey(k => new { k.OrderId, k.ItemId });

			modelBuilder.Entity<OrderItem>()
						.HasOne(x => x.Item)
						.WithMany(y => y.OrderItems)
						.HasForeignKey(z => z.ItemId)
						.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
