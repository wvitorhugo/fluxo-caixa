using FluxoCaixa.Movimentacao.Domain.Entities;
using FluxoCaixa.Movimentacao.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.Infra.Repositories
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(DbSet<Lancamento> lancamentos) : base (lancamentos)
        { 
        } 
    }
}
