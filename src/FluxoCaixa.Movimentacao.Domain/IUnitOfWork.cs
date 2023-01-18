using FluxoCaixa.Movimentacao.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.Domain
{

    public interface IUnitOfWork : IDisposable
    {
        ILancamentoRepository LancamentoRepository { get; set; } 
        Task<int> SaveAsync();
    }

}