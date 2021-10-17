using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.Interfaces
{
    public interface ICountryService
    {
        Task<List<CountryListResponse>> GetCountryList(CountryListRequest countryListRequest);

        Task<List<BankListResponse>> GetBanksList(BankListRequest bankListRequest);

        Task<List<StateListResponse>> GetStateList(StateListRequest stateListRequest);

        Task<ExchangeRateResponse> GetExchangeRate(ExchangeRateRequest exchangeRateRequest);

        Task<List<FeesListResponse>> GetFeeList(FeesListRequest feeListRequest);
    }
}
