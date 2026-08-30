using System.Net;
using UploadImagemR2.DTOs.Aplicacoes;
using UploadImagemR2.Exceptions;
using UploadImagemR2.Interfaces;
using UploadImagemR2.Models;
using UploadImagemR2.Services.Interfaces;

namespace UploadImagemR2.Services
{
    public class AplicacaoService : IAplicacaoService
    {
        private readonly ApiKeyService _apiKey;
        private readonly IAplicacaoRepository _repository;
        public AplicacaoService(ApiKeyService apiKey, IAplicacaoRepository repository)
        {
            _apiKey = apiKey;
            _repository = repository;
        }
        public async Task<AplicacaoResponse> CreateAsync(CreateAplicacaoRequest aplicacaoRequest)
        {
            if (string.IsNullOrWhiteSpace(aplicacaoRequest.Nome))
            {
                throw new AppException("O campo nome é obrigatório.", HttpStatusCode.BadRequest);
            }

            var apiKey = _apiKey.GenerateApiKey(ApiKeyEnviroment.Live);
            var hash = _apiKey.HashApiKey(apiKey);

            var aplicacao = new Aplicacao
            {
                Nome = aplicacaoRequest.Nome,
                ApiKeyHash = hash,
                DataCriacao = DateTime.UtcNow
            };

            await _repository.AddAsync(aplicacao);

            return new AplicacaoResponse
            {
                Id = aplicacao.Id,
                Nome = aplicacao.Nome,
                ApiKey = apiKey
            };
        }

        public async Task<AplicacaoResponse> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new AppException("O campo id é obrigatório", HttpStatusCode.BadRequest);
            }

            var aplicacao = await _repository.GetByIdAsync(id);

            if (aplicacao is null)
            {
                throw new AppException("Aplicação não encontrada.", HttpStatusCode.NotFound);
            }

            return new AplicacaoResponse
            {
                Id = aplicacao.Id,
                Nome = aplicacao.Nome
            };
        }

        public async Task<AplicacaoResponse> GetByApiKeyHashAsync(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new AppException("O campo hash é obrigatório", HttpStatusCode.BadRequest);
            }

            var aplicacao = await _repository.GetByApiKeyHashAsync(hash);

            if (aplicacao is null)
            {
                throw new AppException("Aplicação não encontrada.", HttpStatusCode.NotFound);
            }

            return new AplicacaoResponse
            {
                Id = aplicacao.Id,
                Nome = aplicacao.Nome
            };
        }

        public async Task<Aplicacao?> AuthenticateApiKeyAsync(string apiKey)
        {
            var hash = _apiKey.HashApiKey(apiKey);
            var aplicacao = await _repository.GetByApiKeyHashAsync(hash);

            if (aplicacao is null)
            {
                throw new AppException("Aplicação não encontrada.", HttpStatusCode.Unauthorized);
            }

            if (!aplicacao.Ativa)
            {
                throw new AppException("Aplicação não encontrada.", HttpStatusCode.Unauthorized);
            }

            return aplicacao;
        }
    }
}