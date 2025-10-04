using Application.AccountApp;
using Application.CategoryApp;
using Application.CommentApp;
using Application.NewsApp;
using Application.ProductApp;
using Application.ProductPicApp;
using Application.SaleApp;
using Application.TotalSaleApp;
using Application.TransportCostApp;
using Application.UserApp;
using Domain.CategoryAgg;
using Domain.CommentAgg;
using Domain.NewsAgg;
using Domain.ProductAgg;
using Domain.ProductPicAgg;
using Domain.SaleAgg;
using Domain.TotalSaleAgg;
using Domain.TransportCostAgg;
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
			services.AddScoped<IUserApplication, UserApplication>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<IProductApplication, ProductApplication>();
			services.AddScoped<IProductRepository, ProductRepository>();

			services.AddScoped<IProductPicApplication, ProductPicApplication>();
			services.AddScoped<IProductPicRepository, ProductPicRepository>();

			services.AddScoped<ISaleApplication, SaleApplication>();
			services.AddScoped<ISaleRepository, SaleRepository>();

			services.AddScoped<ITotalSaleApplication, TotalSaleApplication>();
			services.AddScoped<ITotalSaleRepository, TotalSaleRepository>();

            services.AddScoped<ITransportCostApplication, TransportCostApplication>();
            services.AddScoped<ITransportCostRepository, TransportCostRepository>();

            services.AddScoped<INewsApplication, NewsApplication>();
            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<ICategoryApplication, CategoryApplication>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<ICommentApplication, CommentApplication>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            services.AddTransient<IAccountApplication, AccountApplication>();
			services.AddTransient<IPasswordHasher, PasswordHasher>();

			services.AddTransient<IPasswordHasher, PasswordHasher>();

		}
	}
}
