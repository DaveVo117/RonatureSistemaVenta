using Microsoft.AspNetCore.Mvc;

using AutoMapper;
//using Newtonsoft.Json;
using Ronature.AplicacionWeb.Models.ViewModels;
using Ronature.AplicacionWeb.Utilidades.Response;
using Ronature.BLL.Interfaces;
using Ronature.Entity;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;

namespace Ronature.AplicacionWeb.Controllers
{
    //Seguridad de Inicio de Sesion
    [Authorize]

    public class CategoriaController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(IMapper mapper, ICategoriaService categoriaService)//Constructor
        {
            _mapper= mapper;
            _categoriaService= categoriaService;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult>Lista()
        {

            List<VMCategoria> vmCategoriaLista = _mapper.Map<List<VMCategoria>>(await _categoriaService.Lista());//Convierte lista de CategoriaService en Lista de VMCategoría
            return StatusCode(StatusCodes.Status200OK, new { data = vmCategoriaLista.OrderBy(x=>x.IdCategoria)});  //la propiedad data se ocupa para llenar un datatable

        }




        [HttpPost]
        public async Task<IActionResult>Crear([FromBody]VMCategoria modelo)
        {

            GenericResponse<VMCategoria> genericResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoria_creada = await _categoriaService.Crear(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoria_creada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex) 
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }




        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VMCategoria modelo)
        {

            GenericResponse<VMCategoria> genericResponse = new GenericResponse<VMCategoria>();

            try
            {
                Categoria categoria_editada = await _categoriaService.Editar(_mapper.Map<Categoria>(modelo));
                modelo = _mapper.Map<VMCategoria>(categoria_editada);

                genericResponse.Estado = true;
                genericResponse.Objeto = modelo;
            }
            catch (Exception ex)
            {
                genericResponse.Estado = false;
                genericResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, genericResponse);

        }




        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idCategoria)
        {

            GenericResponse<string> genericResponse = new GenericResponse<string>();

            try
            {

                genericResponse.Estado = await _categoriaService.Eliminar(idCategoria);

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
