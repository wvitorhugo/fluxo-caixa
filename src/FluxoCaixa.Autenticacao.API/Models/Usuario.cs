using System;

namespace FluxoCaixa.Autenticacao.API.Models
{ 
    public class Usuario
    {
        public Guid idUsuario { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}
