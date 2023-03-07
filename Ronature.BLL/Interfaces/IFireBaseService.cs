﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ronature.BLL.Interfaces
{
  public interface IFireBaseService
  {

    Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo);
    Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo);

  }
}
