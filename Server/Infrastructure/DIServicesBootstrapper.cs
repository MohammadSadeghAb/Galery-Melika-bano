using Application.AccountApp;
using Application.CategoryApp;
using Application.ProductApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Domain.ProductAgg;
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

			services.AddTransient<ICategoryApplication, CategoryApplication>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IAccountApplication, AccountApplication>();
			services.AddTransient<IPasswordHasher, PasswordHasher>();

			services.AddTransient<IPasswordHasher, PasswordHasher>();

		}
	}
}
