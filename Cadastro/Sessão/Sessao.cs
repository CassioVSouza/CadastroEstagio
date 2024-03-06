using Cadastro.Logs;
using Cadastro.Models;
using System.Text.Json;

namespace Cadastro.Sessão
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ISistemaLog _log;
        public Sessao(IHttpContextAccessor httpContextAccessor, ISistemaLog sistemaLog) 
        {
            _contextAccessor = httpContextAccessor;
            _log = sistemaLog;
        }
        public void CriarSessao(UsuarioModel usuario) 
        {
            try
            {
                var stringSessao = JsonSerializer.Serialize(usuario);
                _contextAccessor?.HttpContext?.Session.SetString("ContaAtual", stringSessao);
            }
            catch (Exception ex)
            {
                _log.EscreverLog("Erro em Sessao, função (CriarSessao): " + ex.Message);
            }
        }

        public UsuarioModel PegarSessao() //Verifica se existe alguma sessão atual e retorna o objeto do usuario caso true
        {
            try
            {
                var stringSessao = _contextAccessor?.HttpContext?.Session.GetString("ContaAtual");
                UsuarioModel? usuario = JsonSerializer.Deserialize<UsuarioModel>(stringSessao);

                if(string.IsNullOrEmpty(stringSessao)) { return null; }

                return usuario;
            }
            catch(Exception ex)
            {
                _log.EscreverLog("Erro em Sessao, função (PegarSessao): " + ex.Message);
                return null;
            }
        }

        public void RemoverSessao()
        {
            try
            {
                if(_contextAccessor?.HttpContext?.Session != null)
                {
                    _contextAccessor.HttpContext.Session.Remove("ContaAtual");
                }
            }catch (Exception ex)
            {
                _log.EscreverLog("Erro em Sessao, função (RemoverSessao): " + ex.Message);
            }
        }
    }
}
