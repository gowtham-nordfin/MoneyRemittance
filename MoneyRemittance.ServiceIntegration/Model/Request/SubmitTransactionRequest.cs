namespace MoneyRemittance.ServiceIntegration.Model.Request
{
    public class SubmitTransactionRequest
    {
        public string SenderFirstName { get; set; }
        
        public string SenderLastName { get; set; }
        
        public string SenderEmail { get; set; }
        
        public string SenderPhone { get; set; }
        
        public string SenderAddress { get; set; }
        
        public string SenderCountry { get; set; }
        
        public string SenderCity { get; set; }
        
        public string SendFromState { get; set; }
        
        public string SenderPostalCode { get; set; }
        
        public string DateOfBirth { get; set; }
        
        public string ToFirstName { get; set; }
        
        public string ToLastName { get; set; }
        
        public string ToCountry { get; set; }
        
        public string ToBankAccountName { get; set; }
        
        public string ToBankAccountNumber { get; set; }
        
        public string ToBankName { get; set; }
        
        public string ToBankCode { get; set; }
        
        public string FromAmount { get; set; }
        
        public string ExchangeRate { get; set; }
        
        public string Fees { get; set; }
        
        public string TtransactionNumber { get; set; }
        
        public string FromCurrency { get; set; }
    }
}
