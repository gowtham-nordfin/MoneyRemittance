using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RemittanceProvider.API.Model.Request;
using RemittanceProvider.API.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceProvider.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemittanceController : ControllerBase
    {
        private readonly ILogger<RemittanceController> _logger;
        private readonly IConfiguration _configuration;
        private readonly string accessKey;
        private readonly List<string> _guidList = new()
        {
            "0d3ce1f8-f86d-44e8-8616-476dca2322aa",
            "ac40c4ce-0dbe-4207-9a0f-4e23128982fb",
            "742e31e8-695d-4801-b476-f253ba87275d",
            "419f58e7-91d7-49d3-a54c-b0d6e4416fba",
            "37cecd2a-df70-412f-93de-3fcd510965d1"
        };

        public RemittanceController(ILogger<RemittanceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            accessKey = configuration["AccessKey"];
        }

        [HttpPost("get-exchange-rate")]
        public async Task<IActionResult> GetExchangeRate([FromBody] ExchangeRateRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
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
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("get-country-list")]
        public async Task<IActionResult> GetCountryList([FromBody] CountryListRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
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
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }

        }

        [HttpPost("get-fees-list")]
        public async Task<IActionResult> GetFeesList([FromBody] FeesListRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
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
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("submit-transaction")]
        public async Task<IActionResult> SubmitTransaction([FromBody] SubmitTransactionRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
            {
                var rng = new Random().Next(0, 4);
                var response = new SubmitTransactionResponse
                {
                    TransactionId = _guidList.ElementAt(rng)
                };

                await Task.FromResult(response);

                return rng <= 2 ? Ok(response) : Created("/get-transaction-status", response);
            }
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("get-transaction-status")]
        public async Task<IActionResult> GetTransactionStatus([FromBody] TransctionStatusRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
            {
                var response = new TransactionStatusResponse
                {
                    TransactionId = request.TransactionId
                };

                switch (request.TransactionId)
                {
                    case "0d3ce1f8-f86d-44e8-8616-476dca2322aa":
                    case "ac40c4ce-0dbe-4207-9a0f-4e23128982fb":
                        response.Status = "Success";
                        return Ok(response);
                    case "742e31e8-695d-4801-b476-f253ba87275d":
                        response.Status = "Pending";
                        return Ok(response);
                    case "419f58e7-91d7-49d3-a54c-b0d6e4416fba":
                        response.Status = "Cancelled";
                        return Ok(response);
                    case "37cecd2a-df70-412f-93de-3fcd510965d1":
                        response.Status = "Declined";
                        return Ok(response);
                    default:
                        response.Status = "Pending";
                        return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("get-state-list")]
        public async Task<IActionResult> GetStateList([FromBody] StateListRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
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
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("get-beneficiary-name")]
        public async Task<IActionResult> GetBeneficiaryName([FromBody] BeneficiaryNameRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }
            try
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
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }
        }

        [HttpPost("get-bank-list")]
        public async Task<IActionResult> GetBankList([FromBody] BankListRequest request)
        {
            if (request.Accesskey != accessKey)
            {
                return Unauthorized();
            }

            try
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
            catch (Exception e)
            {
                return StatusCode(440, new ErrorResponse { Error = e?.Message });
            }
        }
    }
}
