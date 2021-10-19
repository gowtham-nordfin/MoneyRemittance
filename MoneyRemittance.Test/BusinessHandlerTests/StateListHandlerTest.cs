using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Country;
using MoneyRemittance.Business.Services.Country.Handlers;
using Moq;
using Xunit;
using System.Linq;

namespace MoneyRemittance.Test.BusinessHandlerTests
{
    public class StateListHandlerTest
    {
        [Fact]
        public async void StateListHandler_GetStatesList()
        {
            //Arange
            var mockCountryService = MockService.GetMockCountryService();
            var mockLogger = new Mock<ILogger<StateListHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            StateListRequest request = new();
            StateListHandler handler = new(mockLogger.Object, mockCountryService.Object, mockConfiguration.Object);

            //Act
            StateListResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.StateList);
            Assert.True(response.StateList.Count == 6);
            Assert.True(response.StateList.All(c =>!string.IsNullOrWhiteSpace(c.Code)));
        }
    }
}
