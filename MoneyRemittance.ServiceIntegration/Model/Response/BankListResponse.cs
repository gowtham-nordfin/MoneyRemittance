using System.Collections.Generic;

namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class BankListResponse : ErrorResponse
    {
       public List<Bank> BankList { get; set; }
    }
}
