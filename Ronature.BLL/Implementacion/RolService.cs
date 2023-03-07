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
  public class RolService : IRolService
  {
    private readonly IGenericRepository<Rol> _repositorio;

    //Constructor de clase
    public RolService(IGenericRepository<Rol> repositorio)
    {
      _repositorio = repositorio;
    }

    //Devuelve la lista de roles
    public async Task<List<Rol>> Lista()
    {
      IQueryable<Rol> query = await _repositorio.Consultar();
      return query.ToList();
    }
  }
}
