using Microsoft.Extensions.Configuration;
using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model;
using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

        public async Task<CountryListResponse> GetCountryList(CountryListRequest countryListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-country-list";
                string jsonString = JsonSerializer.Serialize(countryListRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var countryList = JsonSerializer.Deserialize<List<Country>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return new CountryListResponse
                    {
                        CountryList = countryList
                    };
                }
                else
                {
                    return new CountryListResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BankListResponse> GetBanksList(BankListRequest bankListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-bank-list";
                string jsonString = JsonSerializer.Serialize(bankListRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var bankList = JsonSerializer.Deserialize<List<Bank>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return new BankListResponse
                    {
                        BankList = bankList
                    };
                }
                else
                {
                    return new BankListResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<StateListResponse> GetStateList(StateListRequest stateListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-state-list";
                string jsonString = JsonSerializer.Serialize(stateListRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var stateList = JsonSerializer.Deserialize<List<State>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return new StateListResponse
                    {
                        StateList = stateList
                    };
                }
                else
                {
                    return new StateListResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception)
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
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<ExchangeRateResponse>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    return new ExchangeRateResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FeesListResponse> GetFeeList(FeesListRequest feeListRequest)
        {
            try
            {
                string apiUrl = _baseUrl + "get-fees-list";
                string jsonString = JsonSerializer.Serialize(feeListRequest);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var httpResponse = await _httpClient.PostAsync(apiUrl, content);
                var result = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var feesList = JsonSerializer.Deserialize<List<Fees>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return new FeesListResponse
                    {
                        FeesList = feesList
                    };
                }
                else
                {
                    return new FeesListResponse
                    {
                        Error = result,
                        RequestFailed = true,
                        ResponseCode = httpResponse.StatusCode
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
