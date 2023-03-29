using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ronature.Entity;

namespace Ronature.BLL.Interfaces
{
    public interface INegocioService
    {

        Task<Negocio> Obtener();

        Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null ,string NombreLogo="");



    }
}
