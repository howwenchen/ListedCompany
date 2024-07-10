using System.Reflection;

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
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var interfaceType = serviceType.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IService<>));
            services.AddScoped(interfaceType, serviceType);
        }

        return services;
    }
}
