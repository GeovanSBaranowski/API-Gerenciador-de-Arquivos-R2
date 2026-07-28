using Microsoft.EntityFrameworkCore;
using UploadImagemR2.Models;

namespace UploadImagemR2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Aplicacao> Aplicacoes { get; set; }
    }
}