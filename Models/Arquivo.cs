namespace UploadImagemR2.Models
{
    public class Arquivo
    {
        public int Id { get; set; }
        public string NomeOriginal { get; set; } = string.Empty;
        public string NomeArquivo { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Tamanho { get; set; }

        public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    }
}