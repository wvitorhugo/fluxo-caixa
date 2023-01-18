using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Shared.Autenticacao
{
    public class UsuarioLogado : IUsuarioLogado
    {
        private readonly IHttpContextAccessor _accessor;

        public UsuarioLogado(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Nome => _accessor.HttpContext.User.Identity.Name; 
                   
        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }

        public Guid GetIdUsuario()
        {
            return EstaLogado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetToken()
        {
            return EstaLogado() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public bool EstaLogado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
