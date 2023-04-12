using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Ronature.BLL.Interfaces;
using Ronature.DAL.Interfaces;
using Ronature.Entity;
using System.Globalization;

namespace Ronature.BLL.Implementacion
{
    public class DashBoardService : IDashBoardService
    {

        /*ATRIBUTOS*/
        private readonly IVentaRepository _repositorioVenta;
        private readonly IGenericRepository<DetalleVenta> _repositorioDetalleVenta;
        private readonly IGenericRepository<Categoria> _repositorioCategoria;
        private readonly IGenericRepository<Producto> _repositorioProducto;
        private DateTime _fechainicio = DateTime.Now;

        public DashBoardService(IVentaRepository repositorioVenta, IGenericRepository<DetalleVenta> repositorioDetalleVenta, IGenericRepository<Categoria> repositorioCategoria, IGenericRepository<Producto> repositorioProducto)
        {
            _repositorioVenta = repositorioVenta;
            _repositorioDetalleVenta = repositorioDetalleVenta;
            _repositorioCategoria = repositorioCategoria;
            _repositorioProducto = repositorioProducto;
            _fechainicio = _fechainicio.AddDays(-7);
        }




        /*METODOS*/
        public async Task<int> TotalVentasUltimaSemana()
        {
            try
            {

            IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.FechaRegistro.Value.Date >= _fechainicio.Date);
            int total = query.Count();

            return total;

            }
            catch (Exception)
            {
                throw;
            }

        }




        public async Task<string> TotalIngresosUltimaSemana()
        {
            try
            {

                IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.FechaRegistro.Value.Date >= _fechainicio.Date);

                decimal resultado = query
                    .Select(v => v.Total)
                    .Sum(v => v.Value);

                return Convert.ToString(resultado,new CultureInfo("es-MX"));
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<int> TotalProductos()
        {
            try
            {

                IQueryable<Producto> query = await _repositorioProducto.Consultar();
                int total = query.Count();

                return total;

            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<int> TotalCategorias()
        {
            try
            {

                IQueryable<Categoria> query = await _repositorioCategoria.Consultar();
                int total = query.Count();

                return total;

            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            try
            {

                IQueryable<Venta> query = await _repositorioVenta.Consultar(v => v.FechaRegistro.Value.Date >= _fechainicio.Date);

                Dictionary<string, int> resultado = query
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderByDescending(g => g.Key) //key es la columna que esta especificada en el GroupBy
                    .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                    .ToDictionary(keySelector: r=> r.fecha, elementSelector: r=> r.total);

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Dictionary<string, int>> ProductosTopUltimaSemana()
        {
            try
            {

                IQueryable<DetalleVenta> query = await _repositorioDetalleVenta.Consultar();

                Dictionary<string, int> resultado = query
                    .Include(v=>v.IdVentaNavigation)
                    .Where(dv=>dv.IdVentaNavigation.FechaRegistro.Value.Date >= _fechainicio.Date)
                    .GroupBy(dv => dv.TxtDescripcionProducto).OrderByDescending(g => g.Count()) //agrupa por cantidad de productos vendidos (count) agrupados por txtDescripcion
                    .Select(dv => new { producto = dv.Key,total = dv.Count() }).Take(4)
                    .ToDictionary(keySelector: r => r.producto, elementSelector: r => r.total);

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
        }










    }
}
