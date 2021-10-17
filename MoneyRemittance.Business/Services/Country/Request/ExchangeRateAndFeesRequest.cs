using MoneyRemittance.Business.Shared;

namespace MoneyRemittance.Business.Services.Country

{
    public class ExchangeRateAndFeesRequest : RequestBase<ExchangeRateAndFeesResponse>
    {
        public string From { get; set; }

        public string To { get; set; }

        public double Amount { get; set; }
    }
}
