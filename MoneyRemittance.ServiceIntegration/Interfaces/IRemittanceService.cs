using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.Interfaces
{
    public interface IRemittanceService
    {
        Task<SubmitTransactionResponse> SubmitTransaction(SubmitTransactionRequest submitTransactionRequest);

        Task<TransactionStatusResponse> TransactionStatus(TransctionStatusRequest transactionStatusRequest);
    }
}
