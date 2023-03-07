using Microsoft.AspNetCore.Mvc;

namespace Ronature.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
