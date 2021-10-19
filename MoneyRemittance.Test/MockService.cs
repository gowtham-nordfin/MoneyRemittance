using MoneyRemittance.ServiceIntegration.Interfaces;
using MoneyRemittance.ServiceIntegration.Model.Request;
using Moq;

namespace MoneyRemittance.Test
{
    public class MockService
    {
        public static Mock<ICountryService> GetMockCountryService()
        {
            var mockCountryService = new Mock<ICountryService>();
            mockCountryService.Setup(x => x.GetCountryList(It.IsAny<CountryListRequest>()).Result).Returns(ServiceResponse.GetCountries());
            mockCountryService.Setup(x => x.GetStateList(It.IsAny<StateListRequest>()).Result).Returns(ServiceResponse.GetStates());
            mockCountryService.Setup(x => x.GetBanksList(It.IsAny<BankListRequest>()).Result).Returns((BankListRequest x) =>ServiceResponse.GetBanks(x.Country));
            return mockCountryService;
        }

        public static Mock<ITransactionService> GetMockTransactionService()
        {
            var mockTransactionService = new Mock<ITransactionService>();
            mockTransactionService.Setup(x => x.TransactionStatus(It.IsAny<TransactionStatusRequest>()).Result).Returns((TransactionStatusRequest x) => ServiceResponse.GetTransactionStatus(x.TransactionId));
            mockTransactionService.Setup(x => x.SubmitTransaction(It.IsAny<SubmitTransactionRequest>()).Result).Returns(ServiceResponse.SubmitTransaction());
            return mockTransactionService;
        }
    }
}
