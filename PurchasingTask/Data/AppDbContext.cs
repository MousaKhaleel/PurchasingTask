using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PurchasingTask.Models;

namespace PurchasingTask.Data
{
	public class AppDbContext : IdentityDbContext<Vendor>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Item> Items { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Vendor> Vendors { get; set; }
		public DbSet<PaymentMethod> PaymentMethods { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<OrderItem>()
						.HasKey(k => new { k.OrderId, k.ItemId });

			modelBuilder.Entity<OrderItem>()
						.HasOne(x => x.Item)
						.WithMany(y => y.OrderItems)
						.HasForeignKey(z => z.ItemId)
						.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Order>()
						.Property(e => e.OrderDate)
						.HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<Vendor>().ToTable("Vendors");

			modelBuilder.Entity<Vendor>().Ignore(v => v.EmailConfirmed);
			modelBuilder.Entity<Vendor>().Ignore(v => v.SecurityStamp);
			modelBuilder.Entity<Vendor>().Ignore(v => v.ConcurrencyStamp);
			modelBuilder.Entity<Vendor>().Ignore(v => v.PhoneNumber);
			modelBuilder.Entity<Vendor>().Ignore(v => v.PhoneNumberConfirmed);
			modelBuilder.Entity<Vendor>().Ignore(v => v.TwoFactorEnabled);
			modelBuilder.Entity<Vendor>().Ignore(v => v.LockoutEnd);
			modelBuilder.Entity<Vendor>().Ignore(v => v.LockoutEnabled);

		}
	}
}
