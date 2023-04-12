using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.BLL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Ronature.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
/*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IVentaService _ventaService;

        public ReporteController(IMapper mapper, IVentaService ventaService)//Constructor
        {
            _mapper = mapper;
            _ventaService = ventaService;
        }




        /*METODOS*/
        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public  async Task<IActionResult> ReporteVenta(string fechaIni, string fechaFin)
        {
            List<VMReporteVenta> vmLista = _mapper.Map<List<VMReporteVenta>>( await _ventaService.Reporte(fechaIni, fechaFin));

            return StatusCode(StatusCodes.Status200OK, new { data = vmLista });
        }









    }
}
