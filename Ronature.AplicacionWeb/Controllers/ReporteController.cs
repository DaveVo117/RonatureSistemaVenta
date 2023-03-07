using Microsoft.AspNetCore.Mvc;

namespace Ronature.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
