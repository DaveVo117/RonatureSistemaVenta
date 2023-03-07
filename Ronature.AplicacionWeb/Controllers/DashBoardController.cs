using Microsoft.AspNetCore.Mvc;

namespace Ronature.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
