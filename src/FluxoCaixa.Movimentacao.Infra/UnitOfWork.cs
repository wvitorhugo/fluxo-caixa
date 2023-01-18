using FluxoCaixa.Movimentacao.Domain;
using FluxoCaixa.Movimentacao.Domain.Repositories;
using FluxoCaixa.Movimentacao.Infra.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.Infra
{
    public class UnitOfWork : IUnitOfWork
    {
        public ILancamentoRepository LancamentoRepository { get; set; } 

        private readonly FluxoCaixaContext _fluxoCaixaContext;

        public UnitOfWork(FluxoCaixaContext fluxoCaixaContext, ILancamentoRepository lancamentoRepository)
        {
            _fluxoCaixaContext = fluxoCaixaContext;

            LancamentoRepository = lancamentoRepository; 
        }

        public async Task<int> SaveAsync() => await _fluxoCaixaContext.SaveChangesAsync();

        public void Dispose() => _fluxoCaixaContext.Dispose();
    }
}