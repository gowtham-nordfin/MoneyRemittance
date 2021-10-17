namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class ExchangeRateResponse
    {
        public string SourceCountry { get; set; }

        public string DestinationCountry { get; set; }

        public double ExchangeRate { get; set; }

        public string ExchangeRateToken { get; set; }
    }
}
