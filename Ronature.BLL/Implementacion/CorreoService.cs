using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using Ronature.BLL.Interfaces;
using Ronature.DAL.Interfaces;
using Ronature.Entity;

namespace Ronature.BLL.Implementacion
{
    public class CorreoService : ICorreoService
    {
        private readonly IGenericRepository<Configuracion> _repositorio;

        //Constructor de clase
        public CorreoService(IGenericRepository<Configuracion> repositorio)
        {
            _repositorio = repositorio;
        }

        //envío de correo
        public  async Task<bool> EnviarCorreo(string CorreoDestino, string Asunto, string Mensaje)
        {
            try
            {

                IQueryable<Configuracion> query = await _repositorio.Consultar(c=>c.TxtRecurso.Equals("Servicio_Correo"));
                Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.TxtPropiedad, elementSelector: c=> c.TxtValor);

                var credenciales = new NetworkCredential(Config["correo"], Config["clave"]);
                
                var correo = new MailMessage()
                {
                    From = new MailAddress(Config["correo"], Config["alias"]),
                    Subject = Asunto,
                    Body = Mensaje,
                    IsBodyHtml= true,
                                  
                };

                correo.To.Add(new MailAddress(CorreoDestino));

                var clienteServidor = new SmtpClient()
                {
                    Host = Config ["host"],
                    Port = int.Parse(Config ["puerto"]),
                    Credentials = credenciales,
                    DeliveryMethod=SmtpDeliveryMethod.Network,
                    UseDefaultCredentials= false,
                    EnableSsl = true
                };

            clienteServidor.Send(correo); ;

        return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
