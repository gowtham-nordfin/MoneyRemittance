using System.Collections.Generic;

namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class CountryListResponse : ErrorResponse
    {
        public List<Country> CountryList { get; set; }
    }
}
