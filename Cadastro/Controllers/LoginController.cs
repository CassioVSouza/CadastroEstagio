using Cadastro.Logs;
using Cadastro.Models;
using Cadastro.Repositório;
using Cadastro.Sessão;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuario;
        private readonly ISistemaLog _log;
        private readonly ISessao _sessao;
        public LoginController(IUsuarioRepositorio usuario, ISistemaLog sistemaLog, ISessao sessao) 
        {
            _usuario = usuario;
            _log = sistemaLog;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChecarDados(UsuarioModel usuario) 
        {
            try
            {
                UsuarioModel usuarioDB = _usuario.PegarUsuario(usuario.Email); //Utiliza as infos passadas no front end para buscar um usuario com o msm email no banco
                if (usuarioDB == null)
                {
                    TempData["MensagemDeErro"] = "Esse email não está cadastrado!";
                    return View("Index", usuarioDB);
                }
                if(usuarioDB.Senha == usuario.Senha) //dps compara a senha passada com a senha do banco
                {
                    TempData["MensagemDeSucesso"] = "Bem vindo!";
                    _sessao.CriarSessao(usuarioDB);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemDeErro"] = "Senha incorreta!";
                    return View("Index", usuarioDB);
                }
            }
            catch(Exception ex)
            {
                _log.EscreverLog("Erro em LoginController, função (ChecarDados): " + ex.Message);

                TempData["MensagemDeErro"] = "Não foi possível realizar seu login, tente novamente!";
                return RedirectToAction("Index");
            }
        }
    }
}
