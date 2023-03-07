using System;
using System.Collections.Generic;

namespace Ronature.Entity
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string? TxtCodigoBarra { get; set; }
        public string? TxtMarca { get; set; }
        public string? TxtDescripcion { get; set; }
        public int? IdCategoria { get; set; }
        public int? Stock { get; set; }
        public string? TxtUrlImagen { get; set; }
        public string? TxtNombreImagen { get; set; }
        public decimal? Precio { get; set; }
        public bool? SnActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Categoria? IdCategoriaNavigation { get; set; }
    }
}
