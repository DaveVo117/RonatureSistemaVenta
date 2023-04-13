using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.AplicacionWeb.Utilidades.Response;
using Ronature.BLL.Interfaces;
using Ronature.Entity;
using Microsoft.AspNetCore.Authorization;

namespace Ronature.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class ProductoController : Controller
    {
        /*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public ProductoController(IMapper mapper, IProductoService productoService, ICategoriaService categoriaService)//Constructor
        {
            _mapper = mapper;
            _productoService = productoService;
            _categoriaService = categoriaService;
        }





        /*METODOS*/
        public IActionResult Index()
        {
            return View();
        }





        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<VMProducto> vmProductoLista = _mapper.Map<List<VMProducto>>(await _productoService.Lista());

            return StatusCode(StatusCodes.Status200OK,new {data = vmProductoLista});
        }

        [HttpGet]
        public async Task<IActionResult> ListaCategorias()
        {
            List<VMCategoria> vmListaCategorias = _mapper.Map<List<VMCategoria>>(await _categoriaService.Lista());

            return StatusCode(StatusCodes.Status200OK, new { data = vmListaCategorias });
        }




        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse <VMProducto> genericResponse = new GenericResponse<VMProducto>();

            try
            {
                VMProducto vmProducto = JsonConvert.DeserializeObject<VMProducto>(modelo);

                string nombreImagen = "";
                Stream imagenStream = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

                Producto producto_creado = await _productoService.Crear(_mapper.Map<Producto>(vmProducto),imagenStream,nombreImagen);

                vmProducto = _mapper.Map<VMProducto>(producto_creado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmProducto;

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }







        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] IFormFile imagen, [FromForm] string modelo)
        {
            GenericResponse<VMProducto> genericResponse = new GenericResponse<VMProducto>();

            try
            {
                VMProducto vmProducto = JsonConvert.DeserializeObject<VMProducto>(modelo);

                string nombreImagen = "";
                Stream imagenStream = null;

                if (imagen != null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");
                    string extension = Path.GetExtension(imagen.FileName);
                    nombreImagen = string.Concat(nombre_en_codigo, extension);
                    imagenStream = imagen.OpenReadStream();
                }

                Producto producto_editado= await _productoService.Editar(_mapper.Map<Producto>(vmProducto), imagenStream,  nombreImagen );

                vmProducto = _mapper.Map<VMProducto>(producto_editado);

                genericResponse.Estado = true;
                genericResponse.Objeto = vmProducto;

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }






        [HttpDelete]
        public async Task<IActionResult>Eliminar(int IdProducto)
        {
            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {
                genericResponse.Estado = await _productoService.Eliminar(IdProducto);

            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;

            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);


        }





    }
}
