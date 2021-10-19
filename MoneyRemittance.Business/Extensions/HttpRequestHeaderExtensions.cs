using Microsoft.AspNetCore.Http;
using System;

namespace MoneyRemittance.Business.Extensions
{
    public static class HttpRequestHeaderExtensions
    {
        public const string CorrelationIdHeaderKey = "X-CorrelationId";

        /// <summary>
        /// Get Correlation Id from Header
        /// </summary>
        /// <param name="headerDictionary"></param>
        /// <returns></returns>
        public static Guid GetCorrelationIdFromHeader(this IHeaderDictionary headerDictionary)
        {
            if (headerDictionary.Keys.Contains(CorrelationIdHeaderKey))
            {
                Guid.TryParse(headerDictionary[CorrelationIdHeaderKey], out var correlationId);
                return correlationId;
            }

            return Guid.NewGuid();
        }
    }
}
