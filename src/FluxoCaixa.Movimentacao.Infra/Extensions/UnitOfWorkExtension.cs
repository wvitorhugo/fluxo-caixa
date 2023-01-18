using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using FluxoCaixa.Movimentacao.Infra.Repositories;
using FluxoCaixa.Movimentacao.Infra.Database;
using FluxoCaixa.Movimentacao.Domain;

namespace FluxoCaixa.Movimentacao.Infra.Extensions
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection SetupUnitOfWork([NotNullAttribute] this IServiceCollection serviceCollection)
        { 
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var scopeFactory = f.GetRequiredService<IServiceScopeFactory>();
                var context = f.GetService<FluxoCaixaContext>();
                return new UnitOfWork(
                    context,
                    new LancamentoRepository(context.Lancamentos) 
                );
            });
            return serviceCollection;
        }
    }
}
