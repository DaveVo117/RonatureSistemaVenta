using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Ronature.AplicacionWeb.Controllers
{
    public class PlantillaController : Controller
  {
/*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;
        private readonly IVentaService _ventaService;
    public PlantillaController(IMapper mapper, INegocioService negocioService, IVentaService ventaService)//Constructor
        {
            _mapper = mapper;
            _negocioService = negocioService;
            _ventaService = ventaService;
        }




/*METODOS*/
        public IActionResult EnviarClave(string correo, string clave)
    {
      //Comparte información con la vista
      ViewData["Correo"] = correo;
      ViewData["Clave"] = clave;
      ViewData["Url"] = $"{this.Request.Scheme}://{this.Request.Host}";
      return View();
    }


    public IActionResult RestablecerClave(string clave)
    {
      ViewData["Clave"] = clave;
      return View();
    }



    public async Task<IActionResult> PDFVenta(string numeroVenta)
    {

            VMVenta vmVenta = _mapper.Map<VMVenta>(await _ventaService.Detalle(numeroVenta));
            VMNegocio vmNegocio= _mapper.Map<VMNegocio>(await _negocioService.Obtener());

            VMPDFVenta modelo = new VMPDFVenta();

            modelo.negocio = vmNegocio;
            modelo.venta = vmVenta;

            return View(modelo);
    }



  }
}
