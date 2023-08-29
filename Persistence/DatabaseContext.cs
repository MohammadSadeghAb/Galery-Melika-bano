using Domain.CategoryAgg;
using Domain.NewsAgg;
using Domain.ProductAgg;
using Domain.ProductPicAgg;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

        public DbSet<ProductPic> ProductsPic { get; set; }

        public DbSet<User> Users { get; set; }

		public DbSet<News> News { get; set; }

		public DbSet<Sale> Sales { get; set; }

        public DbSet<TotalSale> TotalSales { get; set; }

        public DbSet<Category> Categories { get; set; }

		public DatabaseContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();	
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

	}
}
