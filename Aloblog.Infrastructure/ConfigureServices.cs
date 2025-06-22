using Aloblog.Application.Interfaces;
using Aloblog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Aloblog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileService, FileService>();
        return services;
    }
}