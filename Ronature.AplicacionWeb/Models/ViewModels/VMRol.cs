using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMRol
    {
        /*Se copia del modelo y se toma solo las propiedades indispensables para interactuar con la vista*/

        //public Rol()
        //{
        //    RolMenus = new HashSet<RolMenu>();
        //    Usuarios = new HashSet<Usuario>();
        //}

        public int IdRol { get; set; }
        public string? TxtDescripcion { get; set; }
        //public bool? SnActivo { get; set; }
        //public DateTime? FechaRegistro { get; set; }

        //public virtual ICollection<RolMenu> RolMenus { get; set; }
        //public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}
