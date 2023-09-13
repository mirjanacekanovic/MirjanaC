using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MirjanaC.Application.Interfaces;
using MirjanaC.Infrastructure.Services;
using MirjanaC.Persistence.Contexts;
using MirjanaC.Persistence.Repositories;
using MrijanaC.Application.Interfaces;
using MrijanaC.Application.Interfaces.Repositories;

namespace MirjanaC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {

            services.AddDbContext<MirjanaContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(MirjanaContext).Assembly.FullName)));

            services.AddScoped<IMirjanaContext>(provider => provider.GetRequiredService<MirjanaContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTimeService, DateTimeService>();

            services
                .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<ICompanyRepository, CompanyRepository>();

            services
                .AddTransient<IMediator, Mediator>()
                .AddTransient<IDateTimeService, DateTimeService>()
                .AddScoped<IMirjanaContext>(provider => provider.GetRequiredService<MirjanaContext>());

            return services;
        }
    }
}
