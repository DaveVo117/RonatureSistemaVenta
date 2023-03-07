using System;
using System.Collections.Generic;

namespace Ronature.Entity
{
    public partial class Negocio
    {
        public int IdNegocio { get; set; }
        public string? TxtUrlLogo { get; set; }
        public string? TxtNombreLogo { get; set; }
        public string? TxtNumeroDocumento { get; set; }
        public string? TxtNombre { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtDireccion { get; set; }
        public string? TxtTelefono { get; set; }
        public decimal? PorcentajeImpuesto { get; set; }
        public string? TxtSimboloMoneda { get; set; }
    }
}
