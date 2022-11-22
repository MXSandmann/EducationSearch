using EducationSearchV3.Models;
using EducationSearchV3.Repositories;
using System.Reflection;

namespace EducationSearchV3.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddEntities(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly
                .GetTypes()
                .Where(c => c
                    .GetInterfaces()
                    .Contains(typeof(IBaseRepository))
                && c.IsClass);
            foreach (var @type in types)
            {
                var @interface = type.GetInterface($"I{type.Name}");
                if (@interface == null)
                    throw new ArgumentException($"The interface for implementation of {type.Name} was mot found");
                services.AddScoped(@interface, @type);
            }
        }
    }    
}
