using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMVenta
    {

        //public Venta()
        //{
        //    DetalleVenta = new HashSet<DetalleVenta>();
        //}

        public int IdVenta { get; set; }
        public string? TxtNumeroVenta { get; set; }
        public int? IdTipoDocumentoVenta { get; set; }
        public int? IdUsuario { get; set; }
        public string? TxtDocumentoCliente { get; set; }
        public string? TxtNombreCliente { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ImpuestoTotal { get; set; }
        public decimal? Total { get; set; }
        public DateTime? FechaRegistro { get; set; }

        //public virtual TipoDocumentoVenta? IdTipoDocumentoVentaNavigation { get; set; }
        //public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<VMDetalleVenta> DetalleVenta { get; set; }

        //Generados en esta pantalla
        public string? TxtTipoDocumentoVenta { get; set; }
        public string? TxtUsuario { get; set; }

    }
}
