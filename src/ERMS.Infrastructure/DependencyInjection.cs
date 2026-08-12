using ERMS.Application.Abstractions.Persistence;
using ERMS.Domain.Users;
using ERMS.Infrastructure.Interceptors;
using ERMS.Infrastructure.Persistence;
using ERMS.Infrastructure.Repositories;
using ERMS.Infrastructure.Services;
using ERMS.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace ERMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<DomainEventsInterceptor>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"));

                options.AddInterceptors(
                    sp.GetRequiredService<DomainEventsInterceptor>());
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>(); 
            services.AddScoped<IUserReadRepository, UserRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();

            return services;
        }
    }
}
