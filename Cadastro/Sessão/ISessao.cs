using Cadastro.Models;
using System.Xml.Serialization;

namespace Cadastro.Sessão
{
    public interface ISessao
    {
        void CriarSessao(UsuarioModel usuario);
        void RemoverSessao();
        UsuarioModel PegarSessao();
    }
}
