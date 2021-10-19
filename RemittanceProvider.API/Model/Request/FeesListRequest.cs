namespace RemittanceProvider.API.Model.Request
{
    public class FeesListRequest : RequestBase
    {
        public string From { get; set; }

        public string To { get; set; }
    }
}
