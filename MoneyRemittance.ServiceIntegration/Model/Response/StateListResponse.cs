using System.Collections.Generic;

namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class StateListResponse : ErrorResponse
    {
        public List<State> StateList { get; set; }
    }
}
