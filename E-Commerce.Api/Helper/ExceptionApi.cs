namespace E_Commerce.Api.Helper
{
    public class ExceptionApi : ResponseApi
    {
        public string Details { get; set; }
        public ExceptionApi(int StatusCode, string Message = null , string Details = null) : base(StatusCode, Message)
        {
            this.Details = Details;
        }
       
    }
}
