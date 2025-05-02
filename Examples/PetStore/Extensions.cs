using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PetStore
{
    public static class Extensions
    {
        public static IServiceCollection AddWeatherAppClient(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new PetStoreOptions();
            configuration.Bind(nameof(PetStoreOptions), options);
            services.Configure<PetStoreOptions>(configuration.GetSection(nameof(PetStoreOptions)));

            services.AddHttpClient<IPetStoreClient, PetStoreClient>(c => c.BaseAddress = new Uri(options.BaseAddress));
            return services;
        }
    }
}
