using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ronature.DAL.DBContext;
using Ronature.DAL.Interfaces;
using Ronature.Entity;


namespace Ronature.DAL.Implementacion
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly RONATURE_DBContext _dbContext;

        public VentaRepository(RONATURE_DBContext dbContext):base(dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<Venta> Registrar(Venta entidad)
        {
            try
            {
                Venta ventaGenerada = new Venta();
                //Se utiliza una transacción para insertar los registros
                using(var transaction= _dbContext.Database.BeginTransaction())
                {


                    try
                    {
                        foreach (DetalleVenta dv in entidad.DetalleVenta)
                        {

                            Producto producto_encontrato = _dbContext.Productos.Where(p=>p.IdProducto==dv.IdProducto).First();
                            
                            producto_encontrato.Stock= producto_encontrato.Stock - dv.Cantidad;
                            _dbContext.Update(producto_encontrato);

                        }
                        await _dbContext.SaveChangesAsync();



                        NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "venta").First();

                        correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                        correlativo.FechaActualizacion = DateTime.Now;

                        _dbContext.NumeroCorrelativos.Update(correlativo);
                        await _dbContext.SaveChangesAsync();



                        string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                        string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                        numeroVenta.Substring(numeroVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);

                        entidad.TxtNumeroVenta = numeroVenta;
                        
                        await _dbContext.AddAsync(entidad);
                        await _dbContext.SaveChangesAsync();



                        ventaGenerada = entidad;

                        //Secompleta la transacción
                        transaction.Commit();   

                    }
                    catch (Exception ex)
                    {
                        //Se regresa la transacción
                        transaction.Rollback();
                        throw ex;
                    }


                }


                return ventaGenerada;


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaIni, DateTime FechaFin)
        {
            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(v => v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaIni.Date &&
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin).ToListAsync();

            return listaResumen;

        }
    }
}
