namespace RemittanceProvider.API.Model.Request
{
    public class ExchangeRateRequest : RequestBase
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
