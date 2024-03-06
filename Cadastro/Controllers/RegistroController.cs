using Cadastro.Logs;
using Cadastro.Models;
using Cadastro.Repositório;
using Cadastro.Sessão;
using Microsoft.AspNetCore.Mvc;

namespace Cadastro.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IUsuarioRepositorio _usuario;
        private readonly ISistemaLog _log;
        private readonly ISessao _sessao;
        public RegistroController(IUsuarioRepositorio usuarioRepositorio, ISistemaLog sistemaLog, ISessao sessao) 
        {
            _usuario = usuarioRepositorio;
            _log = sistemaLog;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(UsuarioModel usuario)
        {
            try
            {
                _usuario.Registrar(usuario); //Adiciona o usuario ao banco de dados
                TempData["MensagemDeSucesso"] = "Cadastro finalizado com sucesso!";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                _log.EscreverLog("Erro em RegistroController, Função (Registrar): " + ex.Message);

                TempData["MensagemDeErro"] = "Não foi possível fazer o seu cadastro!";
                return View("Index");
            }
        }
    }
}
