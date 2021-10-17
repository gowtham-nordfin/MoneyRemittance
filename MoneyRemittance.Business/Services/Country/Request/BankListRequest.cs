using MoneyRemittance.Business.Shared;

namespace MoneyRemittance.Business.Services.Country
{
    public class BankListRequest : RequestBase<BankListResponse>
    {
        public string Country { get; set; }
    }
}
