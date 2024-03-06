using Cadastro.Logs;
using Cadastro.Models;
using System.Text.Json;

namespace Cadastro.Repositório
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        List<UsuarioModel> usuarios = new List<UsuarioModel>();

        private readonly ISistemaLog _log;
        private readonly IHttpContextAccessor _contextAccessor;
        public UsuarioRepositorio(ISistemaLog log, IHttpContextAccessor httpContextAccessor)
        {
            _log = log;
            _contextAccessor = httpContextAccessor;
        }

        public void Registrar(UsuarioModel usuario)
        {
            try
            {
                usuarios.Add(usuario);
                var usuariosLista = JsonSerializer.Serialize(usuarios); //Resolvi criar uma sessão para armazenar as contas e fazer a função do banco de dados
                _contextAccessor?.HttpContext?.Session.SetString("Contas", usuariosLista); //Cria a sessão com os dados das contas
            }
            catch (Exception ex)
            {
                _log.EscreverLog("Erro em UsuarioRepositorio, funcao (Registrar): " + ex.Message);
            }
        }

        public UsuarioModel PegarUsuario(string email) //Procura pelo banco de dados um email que seja igual ao que foi passado, e retorna a conta inteira
        {
            try
            {
                var usuariosSerializados = _contextAccessor?.HttpContext?.Session.GetString("Contas");
                List<UsuarioModel>? usuariosDB = JsonSerializer.Deserialize<List<UsuarioModel>>(usuariosSerializados);

                if(usuariosDB != null)
                {
                    foreach (var conta in usuariosDB)
                    {
                        if (conta.Email == email)
                        {
                            return conta;
                        }
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                _log.EscreverLog("Erro em UsuarioRepositorio, função (PegarUsuario): " + ex.Message);
                return null;
            }
        }

    }
}
