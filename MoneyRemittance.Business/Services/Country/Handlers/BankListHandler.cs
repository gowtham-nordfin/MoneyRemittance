using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Shared;
using MoneyRemittance.ServiceIntegration.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MoneyRemittance.Business.Services.Country.Handlers
{
    public class BankListHandler : MediatingRequestHandler<BankListRequest, BankListResponse>
    {
        private readonly ILogger _logger;
        private ICountryService _countryService;

        public BankListHandler(ILogger<BankListHandler> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        public override async Task<BankListResponse> Handle(BankListRequest request, CancellationToken cancellationToken)
        {
            var bankListRequest = new ServiceIntegration.Model.Request.BankListRequest
            {
                Country = request.Country
            };

            var bankListForCountry = await _countryService.GetBanksList(bankListRequest);

            var bankListResponse = new BankListResponse
            {
                BankList = new List<Model.Bank>()
            };

            foreach (var item in bankListForCountry)
            {
                bankListResponse.BankList.Add(new Model.Bank
                {
                    Code = item.Code,
                    Name = item.Name
                });
            }

            return await Task.FromResult(bankListResponse);
        }
    }
}
