using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Movimentacao.Domain.Entities;
using System; 

namespace FluxoCaixa.Movimentacao.Application.Strategy
{
    public class LancamentoDebitoStrategy : ILancamentoStrategy
    {
        public decimal GetValorTotalAtualizado(Lancamento ultimoLancamento, LancamentoDto lancamentoDto)
        {
            decimal valorTotalAtualizado = (ultimoLancamento == null) ? 0 : ultimoLancamento.ValorTotalAtualizado;

            valorTotalAtualizado -= lancamentoDto.Valor;

            return valorTotalAtualizado;
        }
    }
}
