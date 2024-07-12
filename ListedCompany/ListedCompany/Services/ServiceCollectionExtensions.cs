using System.Reflection;
using ListedCompany.Services.IService;

namespace ListedCompany.Services;

/// <summary>
/// 擴展ServiceCollection，註冊所有的Service
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAllServices(this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IService<>)))
            .Where(t => !t.IsGenericTypeDefinition && !t.IsInterface)
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var interfaceType = serviceType.GetInterfaces()
                .FirstOrDefault(i => !i.IsGenericType);
            services.AddScoped(interfaceType, serviceType);
        }

        return services;
    }
}
