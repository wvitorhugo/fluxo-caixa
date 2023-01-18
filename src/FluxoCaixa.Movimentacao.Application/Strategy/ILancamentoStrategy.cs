using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Movimentacao.Domain.Entities;

namespace FluxoCaixa.Movimentacao.Application.Strategy
{ 
    public interface ILancamentoStrategy
    {
        decimal GetValorTotalAtualizado(Lancamento ultimoLancamento, LancamentoDto lancamentoDto);
    }
}
