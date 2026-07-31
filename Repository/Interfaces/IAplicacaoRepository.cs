using UploadImagemR2.Models;

namespace UploadImagemR2.Interfaces
{
    public interface IAplicacaoRepository
    {
         Task<Aplicacao> AddAsync(Aplicacao aplicacao);
         Task<Aplicacao?> GetByIdAsync(Guid id);
         Task<Aplicacao?> GetByApiKeyHashAsync(string apiKeyHash);
    }
}