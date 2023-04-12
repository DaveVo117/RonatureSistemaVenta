using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ronature.BLL.Interfaces;
using Ronature.DAL.Interfaces;
using Ronature.Entity;

namespace Ronature.BLL.Implementacion
{
    public class VentaService : IVentaService
    {
/*ATRIBUTOS*/
    private readonly IGenericRepository<Producto> _repositorioProducto;
    private readonly IVentaRepository _repositorioVenta;

        public VentaService(IGenericRepository<Producto> repositorioProducto, IVentaRepository repositorioVenta)//Constructor
        {
            _repositorioProducto = repositorioProducto;
            _repositorioVenta = repositorioVenta;
        }



        /*METODOS*/
        public async Task<List<Producto>> ObteberProductos(string busqueda)
        {
            IQueryable<Producto> query = await _repositorioProducto.Consultar(p=>
                p.SnActivo == true &&
                p.Stock > 0 &&
                string.Concat(
                    p.TxtCodigoBarra,
                    p.TxtMarca,
                    p.TxtDescripcion
                    ).Contains(busqueda)
                );  
            return query.Include(c=>c.IdCategoriaNavigation).ToList();
        }





        public async Task<Venta> Registrar(Venta entidad)
        {
            try
            {
                return await _repositorioVenta.Registrar(entidad);
            }
            catch 
            {
                throw;
            }   
        }





        public async Task<List<Venta>> Historial(string numeroVenta, string fechaIni, string fechaFin)
        {
            IQueryable<Venta> query = await _repositorioVenta.Consultar();
       
            fechaIni = fechaIni is null ? "" : fechaIni;
            fechaFin = fechaFin is null ? "" : fechaFin;

            if(fechaIni !="" && fechaFin != "")
            {
                DateTime fecha_ini = DateTime.ParseExact(fechaIni,"dd/MM/yyyy",new CultureInfo("en-MX"));   
                DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy",new CultureInfo("en-MX"));

                return query.Where(v =>
                    v.FechaRegistro.Value.Date >= fecha_ini.Date &&
                    v.FechaRegistro.Value.Date <= fecha_fin.Date
                )
                    .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
                    .Include(u => u.IdUsuarioNavigation)
                    .Include(dv => dv.DetalleVenta)
                    //.ThenInclude(p=>p.IdProductoNavigation)
                    //.Select(pd=> new { txtDescripcionProducto = pd.DetalleVenta.Select(pd=>pd.txt)})
                    .ToList();
            }
            else
            {
                return query.Where(v =>v.TxtNumeroVenta == numeroVenta)
                   .Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
                   .Include(u => u.IdUsuarioNavigation)
                   .Include(dv => dv.DetalleVenta)
                   .ToList();
            }


        }






        public async Task<Venta> Detalle(string numeroVenta)
        {
            IQueryable<Venta> query = await _repositorioVenta.Consultar(v=>v.TxtNumeroVenta == numeroVenta);

           return query.Include(tdv => tdv.IdTipoDocumentoVentaNavigation)
               .Include(u => u.IdUsuarioNavigation)
               .Include(dv => dv.DetalleVenta)
               .First();
        }






    public async Task<List<DetalleVenta>> Reporte(string fechaIni, string fechaFin)
        {
            DateTime fecha_ini = DateTime.ParseExact(fechaIni, "dd/MM/yyyy", new CultureInfo("en-MX"));
            DateTime fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("en-MX"));

            List<DetalleVenta> lista = await _repositorioVenta.Reporte(fecha_ini, fecha_fin);
            return lista;

        }
    }
}
