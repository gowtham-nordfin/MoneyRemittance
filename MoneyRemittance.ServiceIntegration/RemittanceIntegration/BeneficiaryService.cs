using Microsoft.Extensions.Configuration;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System;
using System.Net.Http;
using System.Text;
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

        public async Task<BeneficiaryNameResponse> GetBeneficiaryName(BeneficiaryNameRequest beneficiaryNameRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-beneficiary-name";
                string jsonString = JsonSerializer.Serialize(beneficiaryNameRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, content);
                var result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<BeneficiaryNameResponse>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    return new BeneficiaryNameResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = response.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
