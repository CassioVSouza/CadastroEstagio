using Cadastro.Sessão;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cadastro.Filtros
{
    public class PaginaParaUsuarios : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) //Busca por alguma sessão ativa, se nao encontrar, redireciona o usuario pra tela de login
        {
            var contaAtual = context.HttpContext.Session.GetString("ContaAtual");
            if(contaAtual == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Login" }, { "Action", "Index" } });
            }
            base.OnActionExecuting(context);
        }
    }
}
