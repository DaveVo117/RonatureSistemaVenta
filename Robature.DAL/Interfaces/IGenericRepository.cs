﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace Ronature.DAL.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity: class
    {
        //Métodos asíncronos, devuelven unaEntidad:Entidad- método- esFuncion<respuesta> conocida como filtro.
        Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro);
        Task<TEntity> Crear(TEntity entidad);
        Task<bool> Editar(TEntity editar);
        Task<bool> Eliminar(TEntity editar);
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro=null);
    }
}
