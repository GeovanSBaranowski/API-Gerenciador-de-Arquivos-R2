using System.ComponentModel.DataAnnotations;

namespace UploadImagemR2.DTOs.Aplicacoes
{
    public class CreateAplicacaoRequest
    {
        [Required]
        [MinLength(1)]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
    }
}