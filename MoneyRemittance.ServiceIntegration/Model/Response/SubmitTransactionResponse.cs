namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class SubmitTransactionResponse : ErrorResponse
    {
        public string TransactionId { get; set; }
    }
}
