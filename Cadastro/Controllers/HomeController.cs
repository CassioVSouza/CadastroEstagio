using Cadastro.Filtros;
using Cadastro.Logs;
using Cadastro.Models;
using Cadastro.Sess�o;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cadastro.Controllers
{
    [PaginaParaUsuarios] //C�digo por tr�s na pasta filtros, serve para permitir que apenas usuarios logados entrem na p�gina
    public class HomeController : Controller
    {
        private readonly ISistemaLog _log; //inje��o de dependencia
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao, ISistemaLog sistemaLog)
        {
            _log = sistemaLog;
            _sessao = sessao;
        }

        public IActionResult Index() //pega a sess�o atual para mostrar o nome do usuario
        {
            UsuarioModel usuario = _sessao.PegarSessao();
            return View(usuario);
        }

        public IActionResult SairDaConta() //Apaga a sess�o quando clica na op��o "sair"
        {
            try
            {
                _sessao.RemoverSessao(); 
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex) //tratamento de erro
            {
                _log.EscreverLog("Erro em HomeController, fun��o (SairDaConta): " + ex.Message);
                return RedirectToAction("Index", "Login");
            }
        }
    }
}
