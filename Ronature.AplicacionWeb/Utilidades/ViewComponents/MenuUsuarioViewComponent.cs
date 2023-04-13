using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Ronature.AplicacionWeb.Utilidades.ViewComponents
{
    public class MenuUsuarioViewComponent : ViewComponent //se agrega herencia 
    {//este componente se relaciona con la carpeta Components/MenuUsuario/Default.cshtml

        public async Task<IViewComponentResult> InvokeAsync()//usar este nombre específico
        {

            ClaimsPrincipal claimUser = HttpContext.User;

            string nombreUsuario = "";
            string urlFotoUsuario = "";

            if (claimUser.Identity.IsAuthenticated)
            {
                nombreUsuario = claimUser.Claims
                    .Where(c=>c.Type == ClaimTypes.Name)
                    .Select(c=>c.Value).SingleOrDefault();

                urlFotoUsuario = ((ClaimsIdentity) claimUser.Identity).FindFirst("TxtUrlFoto").Value;
            }   

                ViewData ["nombreUsuario"] = nombreUsuario;
                ViewData ["urlFotoUsuario"] = urlFotoUsuario;

                return View();

        }





    }
}
