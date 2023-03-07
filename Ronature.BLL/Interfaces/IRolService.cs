using Ronature.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronature.BLL.Interfaces
{
  public interface IRolService
  {

    Task<List<Rol>> Lista();

  }
}
