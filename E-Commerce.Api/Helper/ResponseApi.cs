namespace E_Commerce.Api.Helper
{
    public class ResponseApi
    {
        public int StatusCode { get; set; }
        public string?  Message { get; set; }
       

        public ResponseApi(int StatusCode, string Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message ?? GetMessageFromStatusCode(StatusCode);
            
        }
        private string GetMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Ok",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                _ => "Unknown Status" 
            };
        }
    }
}
