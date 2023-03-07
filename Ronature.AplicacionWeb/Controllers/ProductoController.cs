using Microsoft.AspNetCore.Mvc;

namespace Ronature.AplicacionWeb.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
