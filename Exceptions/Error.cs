namespace SimplifiedBankingApi.Exceptions
{
    public class Error
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public Error(string statusCode, string message, string details)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }
    }
}
