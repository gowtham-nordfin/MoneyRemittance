using MoneyRemittance.Business.Shared;

namespace MoneyRemittance.Business.Services.Remittance
{
    public class TransactionStatusRequest : RequestBase<TransactionStatusResponse>
    {
        public string TransactionId { get; set; }
    }
}
