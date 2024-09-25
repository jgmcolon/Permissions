
using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Permissions.Domain.Abstractions;
using Permissions.Domain.Repositories.Permissions;
using Permissions.Infrastructure.Contexts;
using Permissions.Infrastructure.Repositories.Permission;
using Permissions.Shared;
using System;

namespace Permissions.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Appsettings appsetting)
        {
            AddPersistence(services, appsetting);

            AddHealthChecks(services, appsetting);

            return services;
        }


        private static void AddPersistence(IServiceCollection services, Appsettings appsetting)
        {
            var connectionString = appsetting.ConnectionStrings?.DefaultConnection ??
                throw new ArgumentNullException(nameof(appsetting));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            Console.WriteLine($"connectionString: {connectionString}");

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());
            
        }

        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            Console.WriteLine("CHECK: GetPendingMigrations");

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("dbContext.Database.Migrate()");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("Migrate NOT FOUND");
            }
            

            return app;
        }

        private static void AddHealthChecks(IServiceCollection services, Appsettings appsetting)
        {

            services.AddHealthChecks()
                        .AddSqlServer(appsetting.ConnectionStrings?.DefaultConnection!);

        }


    }
}

