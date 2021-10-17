namespace MoneyRemittance.Business.Services.Country
{
    public class ExchangeRateAndFeesResponse
    {
        public string SourceCountry { get; set; }

        public string DestinationCountry { get; set; }

        public double ExchangeRate { get; set; }

        public string ExchangeRateToken { get; set; }

        public double Fee { get;set;}
    }
}
