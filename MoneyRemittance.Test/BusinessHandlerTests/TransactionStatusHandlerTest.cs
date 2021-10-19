using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoneyRemittance.Business.Services.Remittance;
using Moq;
using Xunit;

namespace MoneyRemittance.Test.BusinessHandlerTests
{
    public class TransactionStatusHandlerTest
    {
        [Fact]
        public async void TransactionStatusHandler_GetTransactionStatus_Success()
        {
            //Arange
            var mockTransactionService = MockService.GetMockTransactionService();
            var mockLogger = new Mock<ILogger<TransactionStatusHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            TransactionStatusRequest request = new()
            {
                TransactionId = "0d3ce1f8-f86d-44e8-8616-476dca2322aa"
            };

            TransactionStatusHandler handler = new(mockLogger.Object, mockTransactionService.Object, mockConfiguration.Object);

            //Act
            TransactionStatusResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Status == "Success");
        }

        [Fact]
        public async void TransactionStatusHandler_GetTransactionStatus_Pending()
        {
            //Arange
            var mockTransactionService = MockService.GetMockTransactionService();
            var mockLogger = new Mock<ILogger<TransactionStatusHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            TransactionStatusRequest request = new()
            {
                TransactionId = "742e31e8-695d-4801-b476-f253ba87275d"
            };

            TransactionStatusHandler handler = new(mockLogger.Object, mockTransactionService.Object, mockConfiguration.Object);

            //Act
            TransactionStatusResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Status == "Pending");
        }

        [Fact]
        public async void TransactionStatusHandler_GetTransactionStatus_Cancelled()
        {
            //Arange
            var mockTransactionService = MockService.GetMockTransactionService();
            var mockLogger = new Mock<ILogger<TransactionStatusHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            TransactionStatusRequest request = new()
            {
                TransactionId = "419f58e7-91d7-49d3-a54c-b0d6e4416fba"
            };

            TransactionStatusHandler handler = new(mockLogger.Object, mockTransactionService.Object, mockConfiguration.Object);

            //Act
            TransactionStatusResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Status == "Cancelled");
        }

        [Fact]
        public async void TransactionStatusHandler_GetTransactionStatus_Declined()
        {
            //Arange
            var mockTransactionService = MockService.GetMockTransactionService();
            var mockLogger = new Mock<ILogger<TransactionStatusHandler>>();
            var mockConfiguration = new Mock<IConfiguration>();

            TransactionStatusRequest request = new()
            {
                TransactionId = "37cecd2a-df70-412f-93de-3fcd510965d1"
            };

            TransactionStatusHandler handler = new(mockLogger.Object, mockTransactionService.Object, mockConfiguration.Object);

            //Act
            TransactionStatusResponse response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response);
            Assert.True(response.Status == "Declined");
        }
    }
}
