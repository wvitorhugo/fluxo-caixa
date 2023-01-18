using FluxoCaixa.Movimentacao.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.Application.Interfaces
{
    public interface ILancamentoService
    {
        Task<LancamentoDto> Add(Guid idUsuario, LancamentoDto lancamentoDto);
        Task<LancamentoCompletoDto> Get(int id);
        Task<IEnumerable<LancamentoCompletoDto>> GetAll();
    }
}
