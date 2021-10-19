namespace MoneyRemittance.ServiceIntegration.Model.Request
{
    public class ExchangeRateRequest : RequestBase
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
