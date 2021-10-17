using MediatR;
using System;
using System.Text.Json.Serialization;

namespace MoneyRemittance.Business.Shared
{
    public abstract class RequestBase<TResponse> : IRequest<TResponse> where TResponse : class
    {
        [JsonIgnore]
        public Guid? CorrelationId { get; set; }
    }
}
