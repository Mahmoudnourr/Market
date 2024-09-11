using Market.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Market.Data
{
	public class MarketContext : IdentityDbContext<application_user>
	{
		public MarketContext(DbContextOptions<MarketContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<application_user>().ToTable("users");
			modelBuilder.Entity<IdentityRole>().ToTable("roles");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_roles");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claims");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("User_Logins");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_tokens");
		}
		public DbSet<application_user> users { get; set; }

		public DbSet<category> categories { get; set; }

		public DbSet<discount> discounts { get; set; }

		public DbSet<product> products { get; set; }
		public DbSet<blacked_list_token> blacked_list_tokens { get; set; }
	}
}
