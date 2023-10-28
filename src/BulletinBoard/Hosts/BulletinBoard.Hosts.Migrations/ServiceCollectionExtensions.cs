using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BulletinBoard.Hosts.DbMigrator
{
    /// <summary>
    /// Методы расширения для добавления сервисов.
    /// </summary>
    public static class ServiceCollectionExtensions
    {  
        /// <summary>
        /// Добавление сервисов.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.ConfigureDbConnections(configuration);
            return services;
        }

        /// <summary>
        /// Конфигкрация подключения к БД.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection ConfigureDbConnections(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EFCoreDb");

            service.AddDbContext<MigrationDbContext>(option => option.UseNpgsql(connectionString));
            return service;
        }
    }
}
