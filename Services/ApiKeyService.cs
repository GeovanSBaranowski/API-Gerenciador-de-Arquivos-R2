using System.Security.Cryptography;

namespace UploadImagemR2.Services
{
    public class ApiKeyService : IApiKeyService
    {
        public string GenerateApiKey(ApiKeyEnviroment enviroment)
        {
            byte[] randomBytes = new byte[32];
            RandomNumberGenerator.Fill(randomBytes);

            string randomPart = Convert.ToBase64String(randomBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");

            string apiKey = $"r2_{enviroment}_{randomPart}";

            return apiKey;
        }

        public string HashApiKey(string apiKey)
        {
            byte[] apiKeyBytes = System.Text.Encoding.UTF8.GetBytes(apiKey);
            byte[] hashBytes = SHA256.HashData(apiKeyBytes);
            
            return Convert.ToHexString(hashBytes);
        }
    }
}