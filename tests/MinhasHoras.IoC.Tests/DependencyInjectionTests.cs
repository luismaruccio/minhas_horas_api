using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhasHoras.Application.Services.Interfaces;
using MinhasHoras.Domain.DomainServices.Interfaces;
using MinhasHoras.Infra.Repositories.Interfaces;
using MongoDB.Driver;

namespace MinhasHoras.IoC.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void RegisterServices_ValidConfiguration_RegistersServices()
        {
            Environment.SetEnvironmentVariable("MONGO_CONNECTION", "mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("SECRET_KEY", "my_secret_key");
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            services.RegisterServices(configuration);

            Assert.NotNull(services.BuildServiceProvider().GetService<IUserRepository>());
            Assert.NotNull(services.BuildServiceProvider().GetService<IPasswordHasherService>());
            Assert.NotNull(services.BuildServiceProvider().GetService<IUserService>());
            Assert.NotNull(services.BuildServiceProvider().GetService<IAuthenticationService>());
            Assert.NotNull(services.BuildServiceProvider().GetService<IMongoClient>());
        }

        [Fact]
        public void RegisterServices_MissingMongoConnection_ThrowsException()
        {
            Environment.SetEnvironmentVariable("MONGO_CONNECTION", null);
            Environment.SetEnvironmentVariable("SECRET_KEY", "my_secret_key");
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            Assert.Throws<ArgumentNullException>(() => services.RegisterServices(configuration));
        }

        [Fact]
        public void RegisterServices_MissingSecretKey_ThrowsException()
        {
            Environment.SetEnvironmentVariable("MONGO_CONNECTION", "mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("SECRET_KEY", null);
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            Assert.Throws<ArgumentNullException>(() => services.RegisterServices(configuration));
        }

        [Fact]
        public void RegisterServices_ValidConfiguration_RegistersJwtService()
        {
            Environment.SetEnvironmentVariable("MONGO_CONNECTION", "mongodb://localhost:27017");
            Environment.SetEnvironmentVariable("SECRET_KEY", "my_secret_key");
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder().Build();

            services.RegisterServices(configuration);

            Assert.NotNull(services.BuildServiceProvider().GetService<IJwtService>());
        }
    }
}