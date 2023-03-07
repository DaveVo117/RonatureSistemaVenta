using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMDetalleVenta
    {

        //public int IdDetalleVenta { get; set; }
        //public int? IdVenta { get; set; }
        public int? IdProducto { get; set; }
        public string? TxtMarcaProducto { get; set; }
        public string? TxtDescripcionProducto { get; set; }
        public string? TxtCategoriaProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }

        //public virtual Venta? IdVentaNavigation { get; set; }

    }
}
