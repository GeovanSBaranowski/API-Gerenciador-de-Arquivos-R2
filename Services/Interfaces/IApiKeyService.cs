namespace UploadImagemR2.Services
{
    public interface IApiKeyService
    {
         string GenerateApiKey(ApiKeyEnviroment enviroment);
         string HashApiKey(string apiKey);
    }
}