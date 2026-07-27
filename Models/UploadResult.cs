namespace UploadImagemR2.Models
{
    public class UploadResult
    {
        public string NomeOriginal { get; set; } = string.Empty;
        public string NomeArquivo { get; set; } = string.Empty;
        public long? Tamanho { get; set; }
        public string ContentType { get; set; } = string.Empty;
    }
}