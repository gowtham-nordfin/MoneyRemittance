using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.RemittanceIntegration;

namespace MoneyRemittance.ServiceIntegration.Extensions
{
    public static class StartupExtension
    {
        public static void AddServiceIntegrationServiceCollection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddHttpClient();
            serviceCollection.AddScoped<IRemittanceService, RemittanceService>();
            serviceCollection.AddScoped<ICountryService, CountryService>();
            serviceCollection.AddScoped<IBeneficiaryService, BeneficiaryService>();
        }
    }
}
