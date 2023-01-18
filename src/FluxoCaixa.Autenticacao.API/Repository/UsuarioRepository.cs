using FluxoCaixa.Autenticacao.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluxoCaixa.Autenticacao.API.Repository
{
    public class UsuarioRepository
    {
        public static Usuario Get(string usuario, string senha) 
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios.Add(new Usuario { idUsuario = new Guid("9F9B893E-ED75-42CC-AD89-F891F13BF0C2"),  Username = "wvitorhugo", Email = "wvitorhugo@gmail.com", Nome = "Wanderson de Paula", Senha = "123" });
            listaUsuarios.Add(new Usuario { idUsuario = new Guid("0836EC24-62FD-4BB8-BA12-815551FA29E4"), Username = "guest", Email = "guest@gmail.com", Nome = "guest", Senha = "123" });

            return listaUsuarios.FirstOrDefault(x => x.Username.Equals(usuario) && x.Senha.Equals(senha));

        }
    }
}
