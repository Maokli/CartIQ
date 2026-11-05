using System.Reflection;
using CartIQ.WebApi.Infrastructure.Settings;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace CartIQ.WebApi.Infrastructure;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Discovers all <see cref="IEndpointGroup"/> implementations in <paramref name="assembly"/>
    /// and registers each as a route group with a matching OpenAPI tag. The route prefix defaults
    /// to <c>/api/{ClassName}</c> but can be overridden via <see cref="IEndpointGroup.RoutePrefix"/>.
    /// </summary>
    public static WebApplication MapEndpoints(this WebApplication app, Assembly assembly)
    {
        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                        && t.IsAssignableTo(typeof(IEndpointGroup)));

        foreach (var type in endpointGroupTypes)
        {
            var groupName = type.Name;
            var routePrefix = type.GetProperty(nameof(IEndpointGroup.RoutePrefix))
                    ?.GetValue(null) as string ?? $"/api/{groupName}";
            var group = app.MapGroup(routePrefix).WithTags(groupName);
            
            // TODO: Setup proper anti forgery
            group.DisableAntiforgery();
            type.GetMethod(nameof(IEndpointGroup.Map))!.Invoke(null, [group]);
        }

        return app;
    }

    /// <summary>
    /// Reads the <see cref="CorsSettings"/> definition in appsettings.json
    /// and configures cors according to that definition.
    /// </summary>
    public static CorsPolicyBuilder ConfigureCors(this WebApplication app, CorsPolicyBuilder opt)
    {
        var corsSettings = app.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

        if (corsSettings == null)
        {
            throw new Exception("Cors settings not found! Define CorsSettings in appsettings.json!");
        }
        
        opt.WithOrigins(corsSettings.AllowedOrigins)
            .WithMethods(corsSettings.AllowedMethods)
            .WithHeaders(corsSettings.AllowedHeaders);
        
        return opt;
    }
}