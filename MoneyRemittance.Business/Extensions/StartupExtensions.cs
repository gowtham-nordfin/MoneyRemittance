using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyRemittance.ServiceIntegration.Extensions;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.RemittanceIntegration;
using System.Reflection;

namespace MoneyRemittance.Business.Extensions
{
    public static class StartupExtensions
    {
        public static void AddBusinessLogicServiceCollection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddScoped<ITransactionService, TransactionService>();
            serviceCollection.AddScoped<ICountryService, CountryService>();
            serviceCollection.AddScoped<IBeneficiaryService, BeneficiaryService>();
            serviceCollection.AddServiceIntegrationServiceCollection(configuration);
        }
    }
}
