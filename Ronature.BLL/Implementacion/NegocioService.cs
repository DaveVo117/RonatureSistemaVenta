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
    public class NegocioService : INegocioService
    {
    /*ATRIBUTOS*/
        private readonly IGenericRepository<Negocio> _repositorio;
        private readonly IFireBaseService _firebaseService;
    /*CONSTRUCTOR*/
        public NegocioService(IGenericRepository<Negocio> repositorio, IFireBaseService firebaseService)
        {
            _repositorio= repositorio;
            _firebaseService= firebaseService;
        }



    /*METODOS*/
        public async Task<Negocio> Obtener()
        {
            try
            {

                Negocio negocio_encontrado = await _repositorio.Obtener(n => n.IdNegocio == 1);
                return negocio_encontrado;

            }
            catch
            {

                throw;
            }
        }





        public async Task<Negocio> GuardarCambios(Negocio entidad, Stream Logo = null, string NombreLogo = "")
        {
            try
            {

                Negocio negocio_encontrado = await _repositorio.Obtener(n=>n.IdNegocio==1);//EN este sistema siempre se va a trabajar con el negocio con ID = 1

                negocio_encontrado.TxtNumeroDocumento = entidad.TxtNumeroDocumento;
                negocio_encontrado.TxtNombre= entidad.TxtNombre;
                negocio_encontrado.TxtCorreo= entidad.TxtCorreo;
                negocio_encontrado.TxtDireccion= entidad.TxtDireccion;
                negocio_encontrado.TxtTelefono= entidad.TxtTelefono;
                negocio_encontrado.PorcentajeImpuesto = entidad.PorcentajeImpuesto;
                negocio_encontrado.TxtSimboloMoneda = entidad.TxtSimboloMoneda;

                negocio_encontrado.TxtNombreLogo = negocio_encontrado.TxtNombreLogo == "" ? NombreLogo : negocio_encontrado.TxtNombreLogo;

                if (Logo != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(Logo, "carpeta_logo", negocio_encontrado.TxtNombreLogo);
                    negocio_encontrado.TxtUrlLogo= urlFoto;
                }

                await _repositorio.Editar(negocio_encontrado);
                return negocio_encontrado;

            }
            catch 
            {

                throw;
            }
        }


    }
}
