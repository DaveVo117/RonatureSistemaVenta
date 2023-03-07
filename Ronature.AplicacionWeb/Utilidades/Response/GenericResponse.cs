namespace Ronature.AplicacionWeb.Utilidades.Response
{
    //Esta clase genera el formato de respuesta que se utilizará en el sitio web
    public class GenericResponse <TObject>
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
        public TObject? Objeto { get; set; }
        public List<TObject>? ListaObjeto { get; set; }
    }
}
