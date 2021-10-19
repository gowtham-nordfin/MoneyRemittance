using MoneyRemittance.ServiceIntegration.Model;
using MoneyRemittance.ServiceIntegration.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyRemittance.Test
{
    public class ServiceResponse
    {
        private static readonly List<string> _guidList = new()
        {
            "0d3ce1f8-f86d-44e8-8616-476dca2322aa",
            "ac40c4ce-0dbe-4207-9a0f-4e23128982fb",
            "742e31e8-695d-4801-b476-f253ba87275d",
            "419f58e7-91d7-49d3-a54c-b0d6e4416fba",
            "37cecd2a-df70-412f-93de-3fcd510965d1"
        };

        public static CountryListResponse GetCountries()
        {
            var response = new CountryListResponse
            {
                CountryList = new List<Country>
            {
            new Country
            {
                Name = "United States",
                Code = "US"
            },

            new Country
            {
                Name = "Great Britain",
                Code = "GB"
            },

             new Country
            {
                Name = "Germany",
                Code = "DE"
            },

            new Country
            {
                Name = "Sweden",
                Code = "SE"
            },
        }
            };

            return response;
        }

        public static StateListResponse GetStates()
        {
            return new StateListResponse
            {
                StateList = new List<State>
                {
                new State
                {
                   Code = "AL",
                   Name = "Alabama"
                },

                new State
                {
                   Code = "AZ",
                   Name = "Arizona"
                },

                new State
                {
                   Code = "DE",
                   Name = "Delaware"
                },

                new State
                {
                   Code = "OK",
                   Name = "Oklahoma"
                },

                new State
                {
                   Code = "TX",
                   Name = "Texas"
                },

                new State
                {
                   Code = "NY",
                   Name = "New York"
                },
                }
            };
        }

        public static BankListResponse GetBanks(string country)
        {
            var response = new BankListResponse
            {
                BankList = new List<Bank>()
            };
            if (country == "US")
            {
                response.BankList.Add(new Bank
                {
                    Code = "CITI",
                    Name = "Citi Bank"
                });

                response.BankList.Add(new Bank
                {
                    Code = "BOFA",
                    Name = "Bank Of America"
                });

                response.BankList.Add(new Bank
                {
                    Code = "AMEX",
                    Name = "American Express"
                });
            }
            else if (country == "GB")
            {
                response.BankList.Add(new Bank
                {
                    Code = "HSBC",
                    Name = "HSBC Bank"
                });
            }
            else if (country == "SE")
            {
                response.BankList.Add(new Bank
                {
                    Code = "SEB",
                    Name = "SEB Bank"
                });
            }

            return response;
        }

        public static TransactionStatusResponse GetTransactionStatus(string transactionId)
        {
            var response = new TransactionStatusResponse
            {
                TransactionId = transactionId
            };

            response.Status = transactionId switch
            {
                "0d3ce1f8-f86d-44e8-8616-476dca2322aa" or "ac40c4ce-0dbe-4207-9a0f-4e23128982fb" => "Success",
                "742e31e8-695d-4801-b476-f253ba87275d" => "Pending",
                "419f58e7-91d7-49d3-a54c-b0d6e4416fba" => "Cancelled",
                "37cecd2a-df70-412f-93de-3fcd510965d1" => "Declined",
                _ => "Pending",
            };

            return response;
        }

        public static SubmitTransactionResponse SubmitTransaction()
        {
            var rng = new Random().Next(0, 4);
            var response = new SubmitTransactionResponse
            {
                TransactionId = _guidList.ElementAt(rng)
            };

            return response;
        }
    }
}
