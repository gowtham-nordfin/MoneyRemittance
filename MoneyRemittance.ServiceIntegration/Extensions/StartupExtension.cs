using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.RemittanceIntegration;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;

namespace MoneyRemittance.ServiceIntegration.Extensions
{
    public static class StartupExtension
    {
        public static void AddServiceIntegrationServiceCollection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddHttpClient<ICountryService, CountryService>().SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddPolicyHandler(GetRetryPolicy());
            serviceCollection.AddHttpClient<IBeneficiaryService, IBeneficiaryService>().SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddPolicyHandler(GetRetryPolicy());
            serviceCollection.AddHttpClient();

            serviceCollection.AddScoped<ITransactionService, TransactionService>();
            serviceCollection.AddScoped<ICountryService, CountryService>();
            serviceCollection.AddScoped<IBeneficiaryService, IBeneficiaryService>();
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
