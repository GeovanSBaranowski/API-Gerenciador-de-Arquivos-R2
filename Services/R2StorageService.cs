using Microsoft.Extensions.Options;
using UploadImagemR2.Configurations;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using UploadImagemR2.Models;
using UploadImagemR2.Data;
using UploadImagemR2.Exceptions;

namespace UploadImagemR2.Services
{
    public class R2StorageService : IR2StorageService
    {
        private readonly R2Settings _r2Settings;
        private readonly AmazonS3Client _s3Client;
        private readonly AppDbContext _context;

        public R2StorageService(IOptions<R2Settings> options, AppDbContext context)
        {
            _r2Settings = options.Value;

            var credentials = new BasicAWSCredentials(_r2Settings.AccessKey, _r2Settings.SecretKey);

            var config = new AmazonS3Config
            {
                ServiceURL = _r2Settings.Endpoint,
                ForcePathStyle = true,
                AuthenticationRegion = "auto"
            };

            _s3Client = new AmazonS3Client(credentials, config);

            _context = context;

        }

        public async Task<UploadResult> UploadAsync(IFormFile file, Guid aplicacaoId)
        {
            var contentType = file.ContentType;
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension != ".jpg" && extension != ".jpeg")
            {
                throw new ArquivoInvalidoException();
            }

            if (contentType != "image/jpeg")
            {
                throw new ArquivoInvalidoException();
            }

            if (file.Length > 10 * 1024 * 1024)
            {
                throw new ArquivoMuitoGrandeException();
            }

            var arquivo = $"{Guid.NewGuid()}-{file.FileName}";

            using var stream = file.OpenReadStream();

            var request = new PutObjectRequest
            {
                BucketName = _r2Settings.BucketName,
                Key = arquivo,
                InputStream = stream,
                ContentType = file.ContentType,
                DisablePayloadSigning = true,
                DisableDefaultChecksumValidation = true
            };


            await _s3Client.PutObjectAsync(request);

            try
            {
                var arquivoDb = new Arquivo
                {
                    NomeOriginal = file.FileName,
                    NomeArquivo = arquivo,
                    ContentType = file.ContentType,
                    Tamanho = file.Length,
                    DataUpload = DateTime.UtcNow,
                    AplicacaoId = aplicacaoId,
                    NomeBucket = _r2Settings.BucketName
                };

                _context.Arquivos.Add(arquivoDb);

                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                await _s3Client.DeleteObjectAsync(_r2Settings.BucketName, request.Key);
                throw;
            }

            return new UploadResult
            {
                NomeOriginal = file.FileName,
                NomeArquivo = arquivo,
                Tamanho = file.Length,
                ContentType = request.ContentType
            };
        }

        public async Task<DownloadResult> DownloadAsync(string arquivo)
        {
            var request = new GetObjectRequest
            {
                BucketName = _r2Settings.BucketName,
                Key = arquivo
            };

            using var response = await _s3Client.GetObjectAsync(request);
            using var memory = new MemoryStream();

            await response.ResponseStream.CopyToAsync(memory);

            return new DownloadResult
            {
                Bytes = memory.ToArray(),
                ContentType = response.Headers.ContentType,
                FileName = arquivo
            };

        }

        public async Task<List<ListResult>> ListAsync()
        {
            var request = new ListObjectsV2Request
            {
                BucketName = _r2Settings.BucketName
            };

            var response = await _s3Client.ListObjectsV2Async(request);
            var listaObjetos = new List<ListResult>();

            foreach (var arquivo in response.S3Objects)
            {
                listaObjetos.Add(new ListResult
                {
                    Nome = arquivo.Key,
                    Tamanho = arquivo.Size,
                    UltimaModificacao = arquivo.LastModified
                });
            }

            return listaObjetos;
        }

        public async Task<bool> DeleteAsync(string arquivo)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _r2Settings.BucketName,
                Key = arquivo
            };

            await _s3Client.DeleteObjectAsync(request);

            return true;
        }
    }
}