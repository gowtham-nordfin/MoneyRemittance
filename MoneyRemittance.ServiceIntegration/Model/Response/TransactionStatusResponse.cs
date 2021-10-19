namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class TransactionStatusResponse : ErrorResponse
    {
        public string TransactionId { get; set; }

        public string Status { get; set; }
    }
}
