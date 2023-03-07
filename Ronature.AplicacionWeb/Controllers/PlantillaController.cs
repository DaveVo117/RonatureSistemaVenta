using Microsoft.AspNetCore.Mvc;

namespace Ronature.AplicacionWeb.Controllers
{
  public class PlantillaController : Controller
  {
    public IActionResult EnviarClave(string correo, string clave)
    {
      //Comparte información con la vista
      ViewData["Correo"] = correo;
      ViewData["Clave"] = clave;
      ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
      return View();
    }
    public IActionResult REstablecerClave(string clave)
    {
      ViewData["Clave"] = clave;
      return View();
    }
  }
}
