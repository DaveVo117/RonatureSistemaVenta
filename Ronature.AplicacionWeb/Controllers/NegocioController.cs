using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.AplicacionWeb.Utilidades.Response;
using Ronature.BLL.Interfaces;
using Ronature.Entity;

namespace Ronature.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
    /*ATRIBUTOS*/
        private readonly IMapper _mapper;
        private readonly INegocioService _negocioService;

        /*CONSTRUCTOR*/
        public NegocioController(IMapper mapper,INegocioService negocioService)
        {
            _mapper= mapper;
            _negocioService= negocioService;
        }



    /*MÉDTODOS*/
        public IActionResult Index()
        {
            return View();
        }






        [HttpGet]
        public async Task<IActionResult>Obtener()
        {
            GenericResponse<VMNegocio> genericResponse = new GenericResponse<VMNegocio>();

            try
            {
                VMNegocio vmNegocio = _mapper.Map<VMNegocio>(await _negocioService.Obtener());//Convierte la información del objeto Negocio en VMNegocio par adesplegarla en la vista

                genericResponse.Estado = true;
                genericResponse.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = true;
                genericResponse.Mensaje = ex.Message;
                throw;
            }

            return StatusCode(StatusCodes.Status200OK,genericResponse);
        }





        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromForm]IFormFile logo, [FromForm]string modelo)
        {
            GenericResponse<VMNegocio> genericResponse = new GenericResponse<VMNegocio>();

            try
            {
                VMNegocio vmNegocio = JsonConvert.DeserializeObject<VMNegocio>(modelo);//Convierte la información del objeto Negocio en VMNegocio par adesplegarla en la vista

                string nombreLogo = "";
                Stream logoStream= null;

                if(logo!= null)
                {
                    string nombre_en_codigo = Guid.NewGuid().ToString("N");//"N" para obtener números y letras
                    string extension = Path.GetExtension(logo.FileName);
                    nombreLogo = string.Concat(nombre_en_codigo, extension);
                    logoStream = logo.OpenReadStream();
                }

                //GuardaCambios
                Negocio negocio_editado = await _negocioService.GuardarCambios(_mapper.Map<Negocio>(vmNegocio)
                    , logoStream, nombreLogo);

                //Regresa cambios guardados para mostra en la vista
                vmNegocio = _mapper.Map<VMNegocio>(negocio_editado);


                genericResponse.Estado = true;
                genericResponse.Objeto = vmNegocio;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = true;
                genericResponse.Mensaje = ex.Message;
                throw;
            }

            return StatusCode(StatusCodes.Status200OK, genericResponse);
        }






    }
}
