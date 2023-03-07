namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMReporteVenta
    {

        public string? FechaRegistro { get; set; }
        public string? TxtNumeroVenta { get; set; }
        public string? TxtTipoDocumento { get; set; }
        public string? TxtDocumentoCliente { get; set; }
        public string? TxtNombreCliente { get; set; }
        public string? SubTotalVenta { get; set; }
        public string? ImpuestoTotalVenta { get; set; }
        public string? TotalVenta { get; set; }
        public string? Producto { get; set; }
        public int? Cantidad { get; set;}
        public string? Precio { get; set;}
        public string? Total { get; set;}

    }
}
