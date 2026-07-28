namespace UploadImagemR2.Models
{
    public class Arquivo
    {
        public int Id { get; set; }
        public Guid AplicacaoId { get; set; }
        public Aplicacao Aplicacao { get; set; } = null!;
        public string NomeBucket { get; set; } = string.Empty;
        public string NomeOriginal { get; set; } = string.Empty;
        public string NomeArquivo { get; set; } = string.Empty;
        public string Chave { get; set; } = string.Empty;
        public string? OwnerId { get; set; }
        public string? Categoria { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public long Tamanho { get; set; }

        public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    }
}