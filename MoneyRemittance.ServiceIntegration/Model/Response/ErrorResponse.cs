using System.Net;

namespace MoneyRemittance.ServiceIntegration.Model.Response
{
    public class ErrorResponse
    {
        public string Error { get; set; }

        public bool RequestFailed { get; set; }

        public HttpStatusCode ResponseCode { get; set; }
    }
}
