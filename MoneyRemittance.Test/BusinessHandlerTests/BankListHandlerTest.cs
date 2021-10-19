using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Country;
using MoneyRemittance.Business.Services.Country.Handlers;
using Moq;
using Xunit;
using System.Linq;

namespace MoneyRemittance.Test.BusinessHandlerTests
{
    public class BankListHandlerTest
    {
        [Fact]
        public async void BanksListHandler_GetBankList_UnitedStates()
        {
            //Arange
            var mockCountryService = MockService.GetMockCountryService();
            var mockLogger = new Mock<ILogger<BankListHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            BankListRequest request = new()
            {
                Country = "US"
            };
            BankListHandler handler = new(mockLogger.Object, mockCountryService.Object, mockConfiguration.Object);

            //Act
            BankListResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.BankList);
            Assert.True(response.BankList.Count == 3);
            Assert.True(response.BankList.Exists(c =>c.Code == "CITI"));
            Assert.True(response.BankList.Exists(c =>c.Code == "BOFA"));
            Assert.True(response.BankList.Exists(c =>c.Code == "AMEX"));
        }

        [Fact]
        public async void BanksListHandler_GetBankList_GreatBritain()
        {
            //Arange
            var mockCountryService = MockService.GetMockCountryService();
            var mockLogger = new Mock<ILogger<BankListHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            BankListRequest request = new()
            {
                Country = "GB"
            };
            BankListHandler handler = new(mockLogger.Object, mockCountryService.Object, mockConfiguration.Object);

            //Act
            BankListResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.BankList);
            Assert.True(response.BankList.Count == 1);
            Assert.True(response.BankList.Exists(c =>c.Code == "HSBC"));
        }

        [Fact]
        public async void BanksListHandler_GetBankList_Sweden()
        {
            //Arange
            var mockCountryService = MockService.GetMockCountryService();
            var mockLogger = new Mock<ILogger<BankListHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            BankListRequest request = new()
            {
                Country = "SE"
            };
            BankListHandler handler = new(mockLogger.Object, mockCountryService.Object, mockConfiguration.Object);

            //Act
            BankListResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.NotEmpty(response.BankList);
            Assert.True(response.BankList.Count == 1);
            Assert.True(response.BankList.Exists(c =>c.Code == "SEB"));
        }
    }
}
