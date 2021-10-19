namespace MoneyRemittance.ServiceIntegration.Model.Request
{
    public class FeesListRequest : RequestBase
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
