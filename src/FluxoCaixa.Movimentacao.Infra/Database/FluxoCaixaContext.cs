using FluxoCaixa.Movimentacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FluxoCaixa.Movimentacao.Infra.Database
{
    public class FluxoCaixaContext : DbContext
    {
        public DbSet<Lancamento> Lancamentos { get; set; }
        public FluxoCaixaContext(DbContextOptions<FluxoCaixaContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("FluxoCaixaContext");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
