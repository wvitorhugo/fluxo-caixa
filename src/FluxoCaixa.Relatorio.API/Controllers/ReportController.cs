using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluxoCaixa.Relatorio.API.Entities;
using FluxoCaixa.Relatorio.API.Services;
using FluxoCaixa.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FluxoCaixa.Relatorio.API.Controllers
{
    [Authorize]
    public class ReportController : Controller
    { 
        private readonly ILogger<ReportController> _logger;
        private readonly LancamentoServices _lancamentoServices;

        public ReportController(LancamentoServices lancamentoServices, ILogger<ReportController> logger)
        {
            _lancamentoServices = lancamentoServices;
            _logger = logger;
        } 
          
        [HttpGet("report/Lancamentos")]
        public async Task<ActionResult<IEnumerable<Lancamento>>> GetLancamentos(FiltroRelatorio filtroRelatorio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Errors());
                }

                var Lancamento = await _lancamentoServices.GetLancamentos(filtroRelatorio.Ano, filtroRelatorio.Mes, filtroRelatorio.Dia);

                if (Lancamento == null)
                {
                    _logger.LogError($"Lancamentos do dia: {filtroRelatorio.Ano}-{filtroRelatorio.Mes}-{filtroRelatorio.Dia} is not found");
                    return NotFound();
                }

                return Ok(Lancamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("report/Consolidado")]
        public async Task<ActionResult<LancamentoConsolidado>> GetLancamentoConsolidado(FiltroRelatorio filtroRelatorio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Errors());
                }

                var Lancamento = await _lancamentoServices.GetLancamentoConsolidado(filtroRelatorio.Ano, filtroRelatorio.Mes, filtroRelatorio.Dia);
                if (Lancamento == null)
                {
                    _logger.LogError($"Lancamentos do dia: {filtroRelatorio.Ano}-{filtroRelatorio.Mes}-{filtroRelatorio.Dia} is not found");
                    return NotFound();
                }

                return Ok(Lancamento); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}
