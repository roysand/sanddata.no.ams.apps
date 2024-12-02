using Application.Interface;
using DataLayer.Application.Interface;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Config;
using DataLayer.Infrastructure.Interface;
using DataLayer.Infrastructure.Persistence;
using Infrastructure.Clients;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<DataLayer.Application.Interface.IApplicationDbContext,ApplicationDbContext>();
        services.AddTransient<ApplicationDbContext>();
        services.AddTransient<IDetailRepository<Detail>, DetailRepository>();
        services.AddTransient<IMqttManagedClient, AMSMqttManagedClient>();
        services.AddSingleton<IConfig, Config>();
        // services.AddTransient<AMSMqttManagedClient>();
        return services;
    }
}
