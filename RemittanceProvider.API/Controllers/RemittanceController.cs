using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemittanceProvider.API.Model.Request;
using RemittanceProvider.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemittanceProvider.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemittanceController : ControllerBase
    {
        private readonly ILogger<RemittanceController> _logger;

        public RemittanceController(ILogger<RemittanceController> logger)
        {
            _logger = logger;
        }

        [HttpPost("get-exchange-rate")]
        public async Task<IActionResult> GetExchangeRate([FromBody] ExchangeRateRequest request)
        {
            var response = new ExchangeRateResponse
            {
                SourceCountry = request.From,
                DestinationCountry = request.To
            };

            if (request.From == "US" && request.To == "GB")
            {
                response.ExchangeRate = 0.73;
            }
            else if (request.From == "US" && request.To == "DE")
            {
                response.ExchangeRate = 0.86;
            }
            else if (request.From == "US" && request.To == "SE")
            {
                response.ExchangeRate = 8.62;
            }

            await Task.FromResult(response);
            return Ok(response);
        }

        [HttpPost("get-country-list")]
        public async Task<IActionResult> GetCountryList()
        {
            var response = new List<CountryListResponse>();
            await Task.Run(() =>
           {
               var usa = new CountryListResponse
               {
                   Name = "United States",
                   Code = "US"
               };

               var gb = new CountryListResponse
               {
                   Name = "Great Britain",
                   Code = "GB"
               };

               var germany = new CountryListResponse
               {
                   Name = "Germany",
                   Code = "DE"
               };

               var sweden = new CountryListResponse
               {
                   Name = "Sweden",
                   Code = "SE"
               };

               response.Add(usa);
               response.Add(gb);
               response.Add(germany);
               response.Add(sweden);

               return response;
           });

            return Ok(response);
        }

        [HttpPost("get-fees-list")]
        public async Task<IActionResult> GetFeesList([FromBody] FeesListRequest request)
        {
            var response = new List<FeesListResponse>();

            if (request.From == "US" && request.To == "GB")
            {
                response.Add(new FeesListResponse
                {
                    Amount = 1000.00,
                    Fee = 10.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 5000.00,
                    Fee = 50.00
                });
                response.Add(new FeesListResponse
                {
                    Amount = 10000.00,
                    Fee = 100.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 20000.00,
                    Fee = 200.00
                });
            }
            else if (request.From == "US" && request.To == "DE")
            {
                response.Add(new FeesListResponse
                {
                    Amount = 1000.00,
                    Fee = 10.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 5000.00,
                    Fee = 50.00
                });
                response.Add(new FeesListResponse
                {
                    Amount = 10000.00,
                    Fee = 100.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 20000.00,
                    Fee = 200.00
                });
            }
            else if (request.From == "US" && request.To == "SE")
            {
                response.Add(new FeesListResponse
                {
                    Amount = 1000.00,
                    Fee = 100.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 5000.00,
                    Fee = 500.00
                });
                response.Add(new FeesListResponse
                {
                    Amount = 10000.00,
                    Fee = 1000.00
                });

                response.Add(new FeesListResponse
                {
                    Amount = 20000.00,
                    Fee = 2000.00
                });
            }

            await Task.FromResult(response);
            return Ok(response);
        }

        [HttpPost("submit-transaction")]
        public async Task<IActionResult> SubmitTransaction([FromBody] SubmitTransactionRequest request)
        {
            var rng = new Random().Next(1, 10);
            var response = new SubmitTransactionResponse
            {
                TransactionId = Guid.NewGuid().ToString()
            };

            await Task.FromResult(response);

            return rng <= 5 ? Ok(response) : Created("/get-transaction-status", response);
        }

        [HttpPost("get-state-list")]
        public async Task<IActionResult> GetStateList()
        {
            var response = new List<StateListResponse>
            {
                new StateListResponse
                {
                   Code = "AL",
                   Name = "Alabama"
                },

                new StateListResponse
                {
                   Code = "AZ",
                   Name = "Arizona"
                },

                new StateListResponse
                {
                   Code = "DE",
                   Name = "Delaware"
                },

                new StateListResponse
                {
                   Code = "OK",
                   Name = "Oklahoma"
                },

                new StateListResponse
                {
                   Code = "TX",
                   Name = "Texas"
                },

                new StateListResponse
                {
                   Code = "NY",
                   Name = "New York"
                },
            };

            await Task.FromResult(response);
            return Ok(response);
        }

        [HttpPost("get-beneficiary-name")]
        public async Task<IActionResult> GetBeneficiaryName([FromBody] BeneficiaryNameRequest request)
        {
            var response = new BeneficiaryNameResponse();

            if (request.AccountNumber == "1122334455" && request.BankCode == "CITI")
            {
                response.AccountName = "John Oliver";
            }
            if (request.AccountNumber == "111222333444" && request.BankCode == "BOFA")
            {
                response.AccountName = "John Rust";
            }
            if (request.AccountNumber == "111122223333" && request.BankCode == "AMEX")
            {
                response.AccountName = "Rust Oliver";
            }
            if (request.AccountNumber == "2233445566" && request.BankCode == "HSBC")
            {
                response.AccountName = "Oliver Rust";
            }
            if (request.AccountNumber == "9988776655" && request.BankCode == "SEB")
            {
                response.AccountName = "John Rust Oliver";
            }

            await Task.FromResult(response);
            return Ok(response);
        }

        [HttpPost("get-bank-list")]
        public async Task<IActionResult> GetBankList([FromBody] BankListRequest request)
        {
            var response = new List<BankListResponse>();

            if (request.Country == "US")
            {
                response.Add(new BankListResponse
                {
                    Code = "CITI",
                    Name = "Citi Bank"
                });

                response.Add(new BankListResponse
                {
                    Code = "BOFA",
                    Name = "Bank Of America"
                });

                response.Add(new BankListResponse
                {
                    Code = "AMEX",
                    Name = "American Express"
                });
            }
            else if (request.Country == "GB")
            {
                response.Add(new BankListResponse
                {
                    Code = "HSBC",
                    Name = "HSBC Bank"
                });
            }
            else if (request.Country == "SE")
            {
                response.Add(new BankListResponse
                {
                    Code = "SEB",
                    Name = "SEB Bank"
                });
            }

            await Task.FromResult(response);
            return Ok(response);
        }
    }
}
