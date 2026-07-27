namespace UploadImagemR2.Exceptions
{
    public class AppException : Exception
    {
        public System.Net.HttpStatusCode StatusCode { get; }
        public AppException(string message, System.Net.HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}