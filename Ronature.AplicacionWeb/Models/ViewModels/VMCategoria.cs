using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMCategoria
    {

        //public Categoria()
        //{
        //    Productos = new HashSet<Producto>();
        //}

        public int IdCategoria { get; set; }
        public string? TxtDescripcion { get; set; }
        public bool? SnActivo { get; set; }
        //public DateTime? FechaRegistro { get; set; }

        //public virtual ICollection<Producto> Productos { get; set; }

    }
}
