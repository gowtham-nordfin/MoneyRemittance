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
    public class RemittanceService : IRemittanceService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public RemittanceService(HttpClient httpClient, IConfiguration configuration)
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
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<SubmitTransactionResponse>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TransactionStatusResponse> TransactionStatus(TransctionStatusRequest transactionStatusRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-transaction-status";
                string jsonString = JsonSerializer.Serialize(transactionStatusRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<TransactionStatusResponse>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
