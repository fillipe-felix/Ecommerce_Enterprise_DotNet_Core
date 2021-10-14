using System.Linq;
using EE.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {

        protected bool ResponsePossuiErros(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                return true;
            }

            return false;
        }
    }
}