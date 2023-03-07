using Ronature.Entity;

namespace Ronature.AplicacionWeb.Models.ViewModels
{
    public class VMUsuario
    {

        public int IdUsuario { get; set; }
        public string? TxtNombre { get; set; }
        public string? TxtCorreo { get; set; }
        public string? TxtTelefono { get; set; }
        public int? IdRol { get; set; }
        public string? TxtNombreRol { get; set; }
        public string? TxtUrlFoto { get; set; }
        public int? SnActivo { get; set; }//Se convierte en entero 
        
    }
}
