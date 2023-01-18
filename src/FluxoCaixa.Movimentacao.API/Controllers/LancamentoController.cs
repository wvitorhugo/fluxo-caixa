using FluxoCaixa.Movimentacao.API.Middlewares;
using FluxoCaixa.Movimentacao.Application.Interfaces;
using FluxoCaixa.Movimentacao.Domain.DTOs;
using FluxoCaixa.Shared.Autenticacao;
using FluxoCaixa.Shared.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FluxoCaixa.Movimentacao.API.Controllers
{
    [Authorize]
    public class LancamentoController : Controller
    {
        private readonly ILancamentoService _lancamentoService;
        private readonly IUsuarioLogado _usuarioLogado;

        public LancamentoController(ILancamentoService lancamentoService, IUsuarioLogado usuarioLogado)
        {
            _lancamentoService = lancamentoService;
            _usuarioLogado = usuarioLogado;
        }

        [HttpGet("lancamento/{id}")]
        public async Task<ActionResult<LancamentoCompletoDto>> Get(int id)
        {
            try
            {
                var result = await _lancamentoService.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("lancamento")]
        public async Task<ActionResult<List<LancamentoCompletoDto>>> GetAll()
        {
            try
            {
                var result = await _lancamentoService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("lancamento")]
        public async Task<ActionResult<LancamentoDto>> Create(LancamentoDto lancamentoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Errors());

                }

                Guid idUsuarioLogado = _usuarioLogado.GetIdUsuario();
                var result = await _lancamentoService.Add(idUsuarioLogado, lancamentoDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
