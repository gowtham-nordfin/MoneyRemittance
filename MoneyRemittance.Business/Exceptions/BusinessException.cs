using System;
using System.Net;

namespace MoneyRemittance.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public string Type { get; }

        public int? Code { get; }

        public Guid? CorrelationId { get; }

        public HttpStatusCode? HttpStatusCode { get; }

        public BusinessException()
        { }

        public BusinessException(string message) : base(message)
        { }

        public BusinessException(string message, int code) : base(message)
        {
            Code = code;
        }

        public BusinessException(string message, int? code, HttpStatusCode? httpStatusCode = null, string type = null) : base(message)
        {
            Code = code;
            HttpStatusCode = httpStatusCode;
            Type = type;
        }

        public BusinessException(Guid? correlationId, string message, int? code, HttpStatusCode? httpStatusCode = null, string type = null) : base(message)
        {
            Code = code;
            HttpStatusCode = httpStatusCode;
            Type = type;
            CorrelationId = correlationId;
        }
    }
}
