using Microsoft.AspNetCore.Mvc;

using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.AplicacionWeb.Utilidades.Response;
using Ronature.BLL.Interfaces;

namespace Ronature.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {
        /*ATRIBUTOS*/
        private readonly IDashBoardService _dashBoardService;

        public DashBoardController(IDashBoardService dashBoardService)
        {
            _dashBoardService = dashBoardService;
        }




        /*METODOS*/
        [HttpGet]
        public async Task<IActionResult> ObtenerResumen()
        {
                GenericResponse<VMDashBoard> gResponse = new GenericResponse<VMDashBoard>();
            try
            {

                VMDashBoard vmDashBoard = new VMDashBoard();

                vmDashBoard.TotalVentas = await _dashBoardService.TotalVentasUltimaSemana();
                vmDashBoard.TotalIngresos = await _dashBoardService.TotalIngresosUltimaSemana();
                vmDashBoard.TotalProductos = await _dashBoardService.TotalProductos();
                vmDashBoard.TotalCategorias = await _dashBoardService.TotalCategorias();

                List<VMVentasSemana> listaVentasSemana = new List<VMVentasSemana>();
                List<VMProductosSemana> listaProductosSemana = new List<VMProductosSemana>();

                foreach (KeyValuePair<string,int> item in await _dashBoardService.VentasUltimaSemana())
                {
                        listaVentasSemana.Add(new VMVentasSemana()
                        {
                            Fecha=item.Key,
                            Total=item.Value
                        });
                }

                foreach (KeyValuePair<string, int> item in await _dashBoardService.ProductosTopUltimaSemana())
                {
                    listaProductosSemana.Add(new VMProductosSemana()
                    {
                        TxtProducto = item.Key,
                        Cantidad = item.Value
                    });
                }


                vmDashBoard.VentasUltimaSemana=listaVentasSemana;
                vmDashBoard.ProductosTopUltimaSemana = listaProductosSemana;

                gResponse.Estado = true;
                gResponse.Objeto = vmDashBoard;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }

            return StatusCode(StatusCodes.Status200OK, gResponse);
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
