using Application.AccountApp;
using Application.CategoryApp;
using Application.ProductApp;
using Application.ProductPicApp;
using Application.SaleApp;
using Application.TotalSaleApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Domain.ProductAgg;
using Domain.ProductPicAgg;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Domain.Users;
using Framework.Password;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Infrastructure
{
	public class DIServicesBootstrapper
	{
		internal static void ServicesBootstrapper(IServiceCollection services)
		{
			services.AddTransient<IUserApplication, UserApplication>();
			services.AddTransient<IUserRepository, UserRepository>();

			services.AddTransient<IProductApplication, ProductApplication>();
			services.AddTransient<IProductRepository, ProductRepository>();

			services.AddTransient<IProductPicApplication, ProductPicApplication>();
			services.AddTransient<IProductPicRepository, ProductPicRepository>();

			services.AddTransient<ISaleApplication, SaleApplication>();
			services.AddTransient<ISaleRepository, SaleRepository>();

			services.AddTransient<ITotalSaleApplication, TotalSaleApplication>();
			services.AddTransient<ITotalSaleRepository, TotalSaleRepository>();

			services.AddTransient<ICategoryApplication, CategoryApplication>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();

			services.AddTransient<IAccountApplication, AccountApplication>();
			services.AddTransient<IPasswordHasher, PasswordHasher>();

			services.AddTransient<IPasswordHasher, PasswordHasher>();

		}
	}
}
