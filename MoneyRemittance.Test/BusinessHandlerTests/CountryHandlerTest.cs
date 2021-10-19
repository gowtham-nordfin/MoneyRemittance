using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Country;
using MoneyRemittance.Business.Services.Country.Handlers;
using Moq;
using Xunit;
using System.Linq;

namespace MoneyRemittance.Test.BusinessHandlerTests
{
    public class CountryListHandlerTest
    {
        [Fact]
        public async void CountryListHandler_GetCountriesList()
        {
            //Arange
            var mockCountryService = MockService.GetMockCountryService();
            var mockLogger = new Mock<ILogger<CountriesListHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            CountryListRequest request = new();
            CountriesListHandler handler = new(mockLogger.Object, mockCountryService.Object, mockConfiguration.Object);

            //Act
            CountryListResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.CountryList);
            Assert.True(response.CountryList.Count == 4);
            Assert.True(response.CountryList.All(c =>!string.IsNullOrWhiteSpace(c.Code)));
        }
    }
}
