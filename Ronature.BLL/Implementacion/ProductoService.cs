using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ronature.BLL.Interfaces;
using Ronature.DAL.Interfaces;
using Ronature.Entity;

namespace Ronature.BLL.Implementacion
{
    public class ProductoService : IProductoService
    {
        /*ATRIBUTOS*/
        private readonly IGenericRepository <Producto> _repositorio;
        private readonly IFireBaseService _firebaseService;


        public ProductoService(IGenericRepository<Producto> repositorio, IFireBaseService firebaseService, IUtilidadesService utilidadesService)//CONSTRUCTOR
        {
            _repositorio = repositorio;
            _firebaseService = firebaseService;
        }



        /*METODOS*/
        public async Task<List<Producto>> Lista()
        {
            IQueryable<Producto> query = await _repositorio.Consultar();
            return query.Include(c=>c.IdCategoriaNavigation).ToList();
        }






        public async Task<Producto> Crear(Producto entidad, Stream imagen = null, string nombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(p => p.TxtCodigoBarra == entidad.TxtCodigoBarra);

            if (producto_existe != null)
                throw new TaskCanceledException("El código de barra ya existe");

            try
            {
                entidad.TxtNombreImagen = nombreImagen;

                if (nombreImagen!=null)
                {
                    string urlImagen = await _firebaseService.SubirStorage(imagen, "carpeta_producto", nombreImagen);
                    entidad.TxtUrlImagen= urlImagen;
                }

                Producto producto_creado = await _repositorio.Crear(entidad);

                if (producto_creado.IdProducto==0)
                    throw new TaskCanceledException("No se pudo crear el producto");

                IQueryable<Producto> query = await _repositorio.Consultar(p => p.IdProducto == producto_creado.IdProducto);

                producto_creado = query.Include(c=>c.IdCategoriaNavigation).First();

                return producto_creado;
            }
            catch (Exception ex)
            {

                throw;
            }
        }






        public async Task<Producto> Editar(Producto entidad, Stream imagen = null, string nombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(p=>p.TxtCodigoBarra == entidad.TxtCodigoBarra && p.IdProducto != entidad.IdProducto);

            if(producto_existe != null)
                throw new TaskCanceledException("El código de barra ya existe");

            try
            {
                IQueryable<Producto> queryProducto = await _repositorio.Consultar(p => p.IdProducto == entidad.IdProducto);

                Producto producto_para_editar = queryProducto.First();

                producto_para_editar.TxtCodigoBarra = entidad.TxtCodigoBarra;
                producto_para_editar.TxtMarca = entidad.TxtMarca;
                producto_para_editar.TxtDescripcion = entidad.TxtDescripcion;
                producto_para_editar.IdCategoria= entidad.IdCategoria;
                producto_para_editar.Stock= entidad.Stock;
                producto_para_editar.Precio = entidad.Precio;
                producto_para_editar.SnActivo = entidad.SnActivo;

                if (producto_para_editar.TxtNombreImagen == "")
                    producto_para_editar.TxtNombreImagen = nombreImagen;

                if (imagen != null)
                {
                    string urlImagen = await _firebaseService.SubirStorage(imagen, "carpeta_producto", producto_para_editar.TxtNombreImagen);
                    producto_para_editar.TxtUrlImagen= urlImagen;
                }

                bool respuesta = await _repositorio.Editar(producto_para_editar);

                if(!respuesta)
                    throw new TaskCanceledException("No se pudo editar el producto");

                Producto producto_editado = queryProducto.Include(c => c.IdCategoriaNavigation).First();

                return producto_editado;

            }
            catch 
            {
                throw;
            }
        }







        public async Task<bool> Eliminar(int IdProducto)
        {
            try
            {
                Producto producto_encontrado = await _repositorio.Obtener(p => p.IdProducto == IdProducto);

                if(producto_encontrado==null)
                    throw new TaskCanceledException("El producto no existe");

                string nombreImagen = producto_encontrado.TxtNombreImagen;
                bool respuesta = await _repositorio.Eliminar(producto_encontrado);

                if (respuesta)
                    await _firebaseService.EliminarStorage("carpeta_producto", nombreImagen);

                return true;

            }
            catch 
            {
                return false;
                throw;
            }
        }


    }
}
