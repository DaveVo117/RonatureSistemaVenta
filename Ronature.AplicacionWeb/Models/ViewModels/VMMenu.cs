using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMMenu
    {

        //public Menu()
        //{
        //    InverseIdMenuPadreNavigation = new HashSet<Menu>();
        //    RolMenus = new HashSet<RolMenu>();
        //}

        //public int IdMenu { get; set; }
        public string? TxtDescripcion { get; set; }
        //public int? IdMenuPadre { get; set; }
        public string? TxtIcono { get; set; }
        public string? TxtControlador { get; set; }
        public string? TxtPaginaAccion { get; set; }
        //public bool? SnActivo { get; set; }
        //public DateTime? FechaRegistro { get; set; }

        //public virtual Menu? IdMenuPadreNavigation { get; set; }
        public virtual ICollection<VMMenu> InverseIdMenuPadreNavigation { get; set; }
        //public virtual ICollection<RolMenu> RolMenus { get; set; }

    }
}
