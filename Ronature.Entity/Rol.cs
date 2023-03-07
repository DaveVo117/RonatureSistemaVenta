using System;
using System.Collections.Generic;

namespace Ronature.Entity
{
    public partial class Rol
    {
        public Rol()
        {
            RolMenus = new HashSet<RolMenu>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string? TxtDescripcion { get; set; }
        public bool? SnActivo { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<RolMenu> RolMenus { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
