namespace MoneyRemittance.ServiceIntegration.Model.Request
{
    public class BeneficiaryNameRequest : RequestBase
    {
        public string AccountNumber { get; set; }

        public string BankCode { get; set; }
    }
}
