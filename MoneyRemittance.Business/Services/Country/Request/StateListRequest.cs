using MoneyRemittance.Business.Shared;

namespace MoneyRemittance.Business.Services.Country
{
    public class StateListRequest : RequestBase<StateListResponse>
    {
        public string Country { get; set; }
    }
}
