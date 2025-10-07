using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hydro.Configuration;

/// <summary>
/// Hydro extensions to IServiceCollection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures services required by
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddHydro(
        this IServiceCollection services,
        [CanBeNull] Action<HydroOptions> configure = null
    ) => services.AddHydro((_, options) => configure?.Invoke(options));

    /// <summary>
    /// Configures services required by
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static IServiceCollection AddHydro(
        this IServiceCollection services,
        [CanBeNull] Action<IServiceProvider, HydroOptions> configure = null
    )
    {
        services.AddSingleton(sp =>
        {
            var options = new HydroOptions();
            configure?.Invoke(sp, options);
            return options;
        });
        services.TryAddSingleton<IPersistentState, PersistentState>();
        return services;
    }
}
