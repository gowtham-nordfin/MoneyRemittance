# Remittance API

Develop a minimal set of APIs which will act as the endpoints for Remittance App. The APIs will consume external APIs provided by the 3rd party remittance provider 

Remittance provider API documentation is available based on which internal APIs will be developed.

# Initial Approach
As per the instructions, the remittance app will have beneficiary management, transaction calculator, transaction status and confirmation screens. 

Looking into the API documentation, the minimal set of API endpoints that can be developed are,
|End Point| Business Need  |
|-- |--|
| Country List | Returns the list of countries supported by remittance provider. This endpoint can be used to show list of countries in Beneficiary management, calculating exchange rates, fees  |
| Bank List | Returns the list of bank for a country. This endpoint can be used to show list of bank in Beneficiary management |
| State List | Returns the list of states for a country. This endpoint can be used to show list of states in Beneficiary management 
| Beneficiary Name | Returns the account name for account number. This endpoint can be used to in Beneficiary management for adding a new beneficiary |
| Exchange Rate & Fees | Returns the current exchange rate and fees for remittance between two countries. Fee is calculated based on the amount to be transferred. Can be used to show real time exchange rate & fees in transaction calculator page|
| Submit Transaction | End point to submit remittance transaction, can be used in transaction screens ||
| Transaction Status | Returns the status of a transaction, can be used in transaction status screen or to send notification to user at later point in time. |

## Considerations & Assumptions
 Since the remittance provider is not a real vendor, tried to create a set of endpoints on my own and consumed it. 
 No database was used, all the data/response from the 3rd party API are hardcoded.

## Project Architecture

Mediator pattern is used to implement the remittance API service. Reasons are,

 - Provides better decoupling between request and response. 
 - Makes the API controller lightweight as bulk of the operations are carried out
   in handler.

**MoneyRemittance.API** - Project where the internal API endpoints are located.
**MoneyRemittance.Business**- Project where all the handlers are located.
**MoneyRemittance.ServiceIntegration** - Project which connects with the 3rd party APIs. This provides one more level of encapsulation with the rest of the application. 
**RemittanceProvider.API** - My implementation of the 3rd party API.

## How to Run

Used API key authorization for accessing the internal API. '0d3ce1f8f86d44e88616476dca2322aa' use this key to access the endpoints. 
Used Open.API features, so the endpoints can be easily tested using the swagger page.

## Conclusion

Have written unit test cases covering few scenarios, haven't done 100% code coverage, since I wanted to show the approach. xUnit & Moq framework is used to write unit test cases.

Implemented retry policy for few of the external API calls. I restricted the retries only to the GET methods.

Exception handling through a middle ware which throws BusinessException.

Logging using Ilogger. Logging the request correlation id which will help us to track the whole request processing from logs.
