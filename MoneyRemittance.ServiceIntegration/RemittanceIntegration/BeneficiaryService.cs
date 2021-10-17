using Microsoft.Extensions.Configuration;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.RemittanceIntegration
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public BeneficiaryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["RemittanceBaseUrl"];
        }

        public async Task<BeneficiaryNameResponse> GetCountryList(BeneficiaryNameRequest beneficiaryNameRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-beneficiary-name";
                string jsonString = JsonSerializer.Serialize(beneficiaryNameRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<BeneficiaryNameResponse>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
