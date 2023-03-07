using System;
using System.Collections.Generic;

namespace Ronature.Entity
{
    public partial class Usuario
    {
        public Usuario()
        {
            Venta = new HashSet<Venta>();
        }

        public int IdUsuario { get; set; }
        public string? TxtNombre { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtTelefono { get; set; }
        public int? IdRol { get; set; }
        public string? TxtUrlFoto { get; set; }
        public string? TxtNombreFoto { get; set; }
        public string? TxtClave { get; set; }
        public bool? SnActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
