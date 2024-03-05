using Cadastro.Filtros;
using Cadastro.Logs;
using Cadastro.Models;
using Cadastro.Sessão;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cadastro.Controllers
{
    [PaginaParaUsuarios] //Código por trás na pasta filtros, serve para permitir que apenas usuarios logados entrem na página
    public class HomeController : Controller
    {
        private readonly ISistemaLog _log; //injeção de dependencia
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao, ISistemaLog sistemaLog)
        {
            _log = sistemaLog;
            _sessao = sessao;
        }

        public IActionResult Index() //pega a sessão atual para mostrar o nome do usuario
        {
            UsuarioModel usuario = _sessao.PegarSessao();
            return View(usuario);
        }

        public IActionResult SairDaConta() //Apaga a sessão quando clica na opção "sair"
        {
            try
            {
                _sessao.RemoverSessao(); 
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex) //tratamento de erro
            {
                _log.EscreverLog("Erro em HomeController, função (SairDaConta): " + ex.Message);
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
