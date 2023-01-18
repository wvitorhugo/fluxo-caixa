using FluxoCaixa.Autenticacao.API.Models;
using FluxoCaixa.Autenticacao.API.Repository;
using FluxoCaixa.Autenticacao.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Autenticacao.API.Controllers
{
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route(template:"login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UsuarioLogin model)
        {
            var usuarioLogado = UsuarioRepository.Get(model.Username, model.Senha);


            if (usuarioLogado == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });


            string token = TokenService.GenerateToken(usuarioLogado);

            usuarioLogado.Senha = string.Empty;

            return new
            {
                Usuario = usuarioLogado, 
                Token = token
            };
        }
    }
}
