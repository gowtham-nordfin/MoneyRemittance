using System.Collections.Generic;

namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class FeesListResponse : ErrorResponse
    {
        public List<Fees> FeesList { get; set; }
    }
}
