using Microsoft.Extensions.Configuration;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.RemittanceIntegration
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CountryService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _baseUrl = configuration["RemittanceBaseUrl"];
        }

        public async Task<List<CountryListResponse>> GetCountryList(CountryListRequest countryListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-country-list";
                string jsonString = JsonSerializer.Serialize(countryListRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<List<CountryListResponse>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<BankListResponse>> GetBanksList(BankListRequest bankListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-bank-list";
                string jsonString = JsonSerializer.Serialize(bankListRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<List<BankListResponse>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<StateListResponse>> GetStateList(StateListRequest stateListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-state-list";
                string jsonString = JsonSerializer.Serialize(stateListRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<List<StateListResponse>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ExchangeRateResponse> GetExchangeRate(ExchangeRateRequest exchangeRateRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-exchange-rate";
                string jsonString = JsonSerializer.Serialize(exchangeRateRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<ExchangeRateResponse>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<FeesListResponse>> GetFeeList(FeesListRequest feeListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-fees-list";
                string jsonString = JsonSerializer.Serialize(feeListRequest);
                string result = await HttpHelper.PostAsync(_httpClient, apiUrl, jsonString);
                return JsonSerializer.Deserialize<List<FeesListResponse>>(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
