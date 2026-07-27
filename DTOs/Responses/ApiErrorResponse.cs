namespace UploadImagemR2.DTOs.Responses
{
    public class ApiErrorResponse
    {
        public int Status { get; set; }
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}