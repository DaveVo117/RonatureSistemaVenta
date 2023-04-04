using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ronature.BLL.Interfaces;
using Ronature.DAL.Interfaces;
using Ronature.Entity;

namespace Ronature.BLL.Implementacion
{
    public class CategoriaService : ICategoriaService
    {
        /*ATRIBUTOS*/
        private readonly IGenericRepository<Categoria> _repositorio;

        public CategoriaService(IGenericRepository<Categoria> repositorio) //Constructor
        {
            _repositorio= repositorio;
        }

        /*METODOS*/
    
        public async Task<List<Categoria>> Lista()
        {
            IQueryable<Categoria> query = await _repositorio.Consultar();
            return query.ToList();
        }


        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                Categoria categoria_creada = await _repositorio.Crear(entidad);

                if(categoria_creada.IdCategoria ==0)
                    throw new TaskCanceledException("No se pudo crear la categoría");

                return categoria_creada;
            }
            catch
            {

                throw;
            }
        }
    

        public async Task<Categoria> Editar(Categoria entidad)
        {
            try
            {
                Categoria categoria_encontrada = await _repositorio.Obtener(c=>c.IdCategoria == entidad.IdCategoria);
                categoria_encontrada.TxtDescripcion = entidad.TxtDescripcion;
                categoria_encontrada.SnActivo = entidad.SnActivo;
                bool respuesta = await _repositorio.Editar(categoria_encontrada);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo nodificar la categoría");

                return categoria_encontrada;
            }
            catch 
            {

                throw;
            }
        }

    
        public async Task<bool> Eliminar(int idCategoria)
        {
            try
            {
                Categoria categoria_encontrada = await _repositorio.Obtener(c => c.IdCategoria == idCategoria);

                if (categoria_encontrada == null)
                    throw new TaskCanceledException("La categoría no existe");
                
                bool respuesta = await _repositorio.Eliminar(categoria_encontrada);

                return respuesta;

            }
            catch
            {

                throw;
            }
        }




    }
}
