using BulletinBoard.Application.AppServices.Contexts.Post.Repositories;
using BulletinBoard.Infrastructure.DataAccess;
using BulletinBoard.Infrastructure.DataAccess.Repositories;
using BulletinBoard.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BulletinBoard.Infrastructure.ComponentRegistrar
{
    public static class Registrar
    {
        public static IServiceCollection ConfigureRepositories(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddScoped<IPostRepository, PostRepository>();
            return services;
        }

        /// <summary>
        /// Добавляет компоненты слоя доступа к данным с помощью EF-Core.
        /// </summary>
        private static IServiceCollection AddDbContext(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ApplicationConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Не найдена строка подключения");
            }

            services.AddDbContext<DataContext>(builder =>
                builder.UseNpgsql(connectionString));

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp =>
                sp.GetRequiredService<DataContext>()));

            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            return services;
        }
    }
}