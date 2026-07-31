using UploadImagemR2.Models;

namespace UploadImagemR2.Services
{
    public interface IR2StorageService
    {
        Task<UploadResult> UploadAsync(IFormFile file);
        Task<DownloadResult> DownloadAsync(string arquivo);
        Task<List<ListResult>> ListAsync();
        Task<bool> DeleteAsync(string arquivo);
    }
}