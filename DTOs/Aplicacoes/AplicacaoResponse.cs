namespace UploadImagemR2.DTOs.Aplicacoes
{
    public class AplicacaoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}