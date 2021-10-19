using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.Interfaces
{
    public interface ICountryService
    {
        Task<CountryListResponse> GetCountryList(CountryListRequest countryListRequest);

        Task<BankListResponse> GetBanksList(BankListRequest bankListRequest);

        Task<StateListResponse> GetStateList(StateListRequest stateListRequest);

        Task<ExchangeRateResponse> GetExchangeRate(ExchangeRateRequest exchangeRateRequest);

        Task<FeesListResponse> GetFeeList(FeesListRequest feeListRequest);
    }
}
