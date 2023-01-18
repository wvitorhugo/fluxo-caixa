
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluxoCaixa.Shared.Autenticacao
{
    public interface IUsuarioLogado
    {
        string Nome { get; }
        Guid GetIdUsuario(); 
        string GetToken();
        bool EstaLogado();
        HttpContext ObterHttpContext();
    }
}
