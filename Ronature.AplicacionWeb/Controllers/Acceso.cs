using Microsoft.AspNetCore.Mvc;

using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.BLL.Interfaces;
using Ronature.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Ronature.AplicacionWeb.Controllers
{
    public class Acceso : Controller
    {
        /*ATRIBUTOS*/
        private readonly IUsuarioService _usuarioService;

        public Acceso(IUsuarioService usuarioService)//Constructor
        {
            _usuarioService = usuarioService;
        }




        /*METODOS*/
        public IActionResult Login()
        {
            return View();
        }
    }
}
