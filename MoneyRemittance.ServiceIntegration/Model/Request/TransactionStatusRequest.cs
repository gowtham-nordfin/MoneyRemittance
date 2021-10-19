namespace MoneyRemittance.ServiceIntegration.Model.Request
{
    public class TransactionStatusRequest : RequestBase
    {
        public string TransactionId { get; set; }
    }
}
