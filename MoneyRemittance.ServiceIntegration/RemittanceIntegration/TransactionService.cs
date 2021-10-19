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
    public class TransactionService : ITransactionService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TransactionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["RemittanceBaseUrl"];
        }

        public async Task<SubmitTransactionResponse> SubmitTransaction(SubmitTransactionRequest submitTransactionRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "submit-transaction";
                string jsonString = JsonSerializer.Serialize(submitTransactionRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<SubmitTransactionResponse>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    return new SubmitTransactionResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TransactionStatusResponse> TransactionStatus(TransactionStatusRequest transactionStatusRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-transaction-status";
                string jsonString = JsonSerializer.Serialize(transactionStatusRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<TransactionStatusResponse>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    return new TransactionStatusResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
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
