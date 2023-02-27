using AMH_MarketPlace.CustomExceptions.Middlewares;
using AMH_MarketPlace.Repositories;
using AMH_MarketPlace.Security.JwtUtils;
using AMH_MarketPlace.Services.Implement;
using AMH_MarketPlace.Services.Implement.AuthImplement;
using AMH_MarketPlace.Services.Implement.StoreImplement;
using AMH_MarketPlace.Services.Implement.StoreImplement.ProductImplement;
using AMH_MarketPlace.Services.Implement.TransactionImplement;
using AMH_MarketPlace.Services.Implement.TransactionImplement.PurchaseProductImplement;
using AMH_MarketPlace.Services.Implement.UserImplement;
using AMH_MarketPlace.Services.Implement.UserImplement.NotifImplement;
using AMH_MarketPlace.Services.Implement.WalletImplement;
using AMH_MarketPlace.Services.Interface;
using AMH_MarketPlace.Services.Interface.AuthInterface;
using AMH_MarketPlace.Services.Interface.StoreInterface;
using AMH_MarketPlace.Services.Interface.StoreInterface.ProductInterface;
using AMH_MarketPlace.Services.Interface.TransactionInterface;
using AMH_MarketPlace.Services.Interface.TransactionInterface.PurchaseProduct;
using AMH_MarketPlace.Services.Interface.UserInterface;
using AMH_MarketPlace.Services.Interface.UserInterface.NotifInterface;
using AMH_MarketPlace.Services.Interface.WalletInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMH_MarketPlace.Extentions
{
    public static class AppConfigExtention
    {
        public static IServiceCollection AppConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Connection Services
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Repository Services
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDbPersistence, DbPersistence>();

            // Service Services
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<ICategoryNotifService, CategoryNotifService>();
            services.AddTransient<INotifReadService, NotifReadService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtUtil, JwtUtil>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IStoreImageService, StoreImageService>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IUserWalletService, UserWalletService>();
            services.AddTransient<ICategoryProductService, CategoryProductService>();
            services.AddTransient<IProductImageService, ProductImageService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IPurchaseProductServie, PurchaseProductService>();

            // Middleware Service
            services.AddScoped<MiddlewareException>();

            // Security Jwt Service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            return services;
        }
    }
}
