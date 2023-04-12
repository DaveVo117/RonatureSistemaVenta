using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.AplicacionWeb.Utilidades.Response;
using Ronature.BLL.Interfaces;
using Ronature.Entity;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using DinkToPdf;
using DinkToPdf.Contracts;

namespace Ronature.AplicacionWeb.Controllers
{
    public class VentaController : Controller
    {
/*ATRIBUTOS*/
        private readonly ITipoDocumentoVentaService _tipoDocumentoVentaService;
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;

        private readonly IConverter _converter;

        public VentaController(ITipoDocumentoVentaService tipoDocumentoVentaService, IVentaService ventaService, IMapper mapper, IConverter converter)//Constructor
        {
            _tipoDocumentoVentaService = tipoDocumentoVentaService;
            _ventaService = ventaService;
            _mapper = mapper;

            _converter = converter;
        }




        /*METODOS*/
        public IActionResult NuevaVenta()
        {
            return View();
        }
        public IActionResult HistorialVenta()
        {
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> ListaTipoDocumentoVenta()
        {
            List<VMTipoDocumentoVenta> vmListaTipoDocumentos = _mapper.Map<List<VMTipoDocumentoVenta>>(await _tipoDocumentoVentaService.Lista());

            return StatusCode(StatusCodes.Status200OK,vmListaTipoDocumentos);
        }





        [HttpGet]
        public async Task<IActionResult> ObtenerProductos(string busqueda)
        {
            List<VMProducto> vmListaProductos = _mapper.Map<List<VMProducto>>(await _ventaService.ObteberProductos(busqueda));

            return StatusCode(StatusCodes.Status200OK, vmListaProductos);
        }





        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] VMVenta modelo)
        {
            GenericResponse<VMVenta> gResponse = new GenericResponse<VMVenta>();

            try
            {
                modelo.IdUsuario = 1; // Se debe pasar el ID_Usuario que se ha logeado al sistema

                Venta venta_creada = await _ventaService.Registrar(_mapper.Map<Venta>(modelo));
                modelo = _mapper.Map<VMVenta>(venta_creada);

                gResponse.Estado = true;
                gResponse.Objeto = modelo;
            }
            catch (Exception ex) 
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }


            return StatusCode(StatusCodes.Status200OK, gResponse);
        }







        [HttpGet]
        public async Task<IActionResult> Historial(string numeroVenta,string fechaIni, string fechaFin)
        {
            List<VMVenta> vmHistorialVenta = _mapper.Map<List<VMVenta>>(await _ventaService.Historial(numeroVenta,fechaIni,fechaFin));

            return StatusCode(StatusCodes.Status200OK, vmHistorialVenta);
        }





        public IActionResult MostrarPDFVenta(string numeroVenta)
        {
            string urlPLantillaVista = $"{this.Request.Scheme}://{this.Request.Host}/Plantilla/PDFVenta?numeroVenta={numeroVenta}";

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings()
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects =
                {
                    new ObjectSettings()
                    {
                        Page = urlPLantillaVista
                    }
                }
            };


            var archivoPDF = _converter.Convert(pdf);

            return File(archivoPDF, "application/pdf");

        }

    }
}
