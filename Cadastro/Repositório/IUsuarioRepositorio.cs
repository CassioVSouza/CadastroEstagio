using Cadastro.Models;

namespace Cadastro.Repositório
{
    public interface IUsuarioRepositorio
    {
        void Registrar(UsuarioModel usuario);
        UsuarioModel PegarUsuario(string email);
    }
}
