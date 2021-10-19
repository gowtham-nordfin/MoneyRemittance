using MoneyRemittance.Business.Shared;

namespace MoneyRemittance.Business.Services.Beneficiary
{
    public class BeneficiaryNameRequest : RequestBase<BeneficiaryNameResponse>
    {
        public string AccountNumber { get; set; }

        public string BankCode { get; set; }
    }
}
