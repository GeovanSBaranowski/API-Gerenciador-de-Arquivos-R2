using Microsoft.EntityFrameworkCore;
using UploadImagemR2.Data;
using UploadImagemR2.DTOs.Aplicacoes;
using UploadImagemR2.Interfaces;
using UploadImagemR2.Models;

namespace UploadImagemR2.Repository
{
    public class AplicacaoRepository : IAplicacaoRepository
    {
        private readonly AppDbContext _context;
        public AplicacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aplicacao> AddAsync(Aplicacao aplicacao)
        {  
            await _context.Aplicacoes.AddAsync(aplicacao);
            await _context.SaveChangesAsync();
            return aplicacao;
        }

        public async Task<Aplicacao?> GetByIdAsync(Guid idAplicacao)
        {
            var aplicacao = await _context.Aplicacoes.FirstOrDefaultAsync(a => a.Id == idAplicacao);

            return aplicacao;
        }

        public async Task<Aplicacao?> GetByApiKeyHashAsync(string apiHashKey)
        {
            var aplicacao = await _context.Aplicacoes.FirstOrDefaultAsync(a => a.ApiKeyHash == apiHashKey);

            return aplicacao;
        }
    }
}