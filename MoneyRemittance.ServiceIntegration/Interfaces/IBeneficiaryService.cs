using MoneyRemittance.ServiceIntegration.Model.Request;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryNameResponse> GetBeneficiaryName(BeneficiaryNameRequest beneficiaryNameRequest);
    }
}
