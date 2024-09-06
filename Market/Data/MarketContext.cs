using Market.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Market.Data
{
	public class MarketContext : IdentityDbContext<ApplicationUser>
	{
		public MarketContext(DbContextOptions<MarketContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> Customers { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<Discount> Discounts { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
