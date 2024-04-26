using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinhasHoras.Application.Services;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.DomainServices;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Domain.Entities;
using MinhasHoras.Infra.MongoDB;
using MinhasHoras.Infra.Repositories;
using MinhasHoras.Infra.Repositories.Interfaces;
using MongoDB.Driver;

namespace MinhasHoras.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoConnection = Environment.GetEnvironmentVariable("MONGO_CONNECTION") ?? throw new ArgumentNullException(nameof(configuration), "MONGO_CONNECTION setting not found in configuration");
            var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY") ?? throw new ArgumentNullException(nameof(configuration), "SECRET_KEY setting not found in configuration");
            services.Configure<MongoSettings>(options =>
            {
                options.ConnectionString = mongoConnection;
                options.DatabaseName = "MinhasHoras";
            });

            services.AddSingleton<IMongoClient>(provider =>
            {
                var settings = provider.GetService<IOptions<MongoSettings>>();

                return new MongoClient(settings!.Value.ConnectionString);
            });

            services.AddScoped(provider =>
            {
                var client = provider.GetService<IMongoClient>();
                var settings = provider.GetService<IOptions<MongoSettings>>();

                return client!.GetDatabase(settings!.Value.DatabaseName);
            });

            //Repositorie
            services.AddScoped<IUserRepository, UserRepository>();

            //Domain Services
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddSingleton<IJwtService>(provider =>
            {                
                return new JwtService(secretKey,"AuthAPI","minhashoras.com", 720);
            });

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        }
    }
}
