using UploadImagemR2.DTOs.Aplicacoes;
using UploadImagemR2.Models;

namespace UploadImagemR2.Services.Interfaces
{
    public interface IAplicacaoService
    {
         Task<AplicacaoResponse> CreateAsync(CreateAplicacaoRequest aplicacaoRequest);
         Task<AplicacaoResponse> GetByIdAsync(Guid id);
         Task<AplicacaoResponse> GetByApiKeyHashAsync(string hash);

         Task<Aplicacao?> AuthenticateApiKeyAsync(string apiKey);
    }
}