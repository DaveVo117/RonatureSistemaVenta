using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ronature.BLL.Interfaces;
using Firebase.Auth;
using Firebase.Storage;
using Ronature.Entity;
using Ronature.DAL.Interfaces;

namespace Ronature.BLL.Implementacion
{
  public class FireBaseService : IFireBaseService
  {

    private readonly IGenericRepository<Configuracion> _repositorio;

    //Constructor de clase
    public FireBaseService(IGenericRepository<Configuracion> repositorio)
    {
      _repositorio = repositorio;
    }


//METODOS
    public async Task<string> SubirStorage(Stream StreamArchivo, string CarpetaDestino, string NombreArchivo)
    {
      string urlImagen = "";
      try
      {
        //Copiado de CorreoService
        IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.TxtRecurso.Equals("FireBase_Storage"));
        Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.TxtPropiedad, elementSelector: c => c.TxtValor);
        //-------------------------

        var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
        var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);
        //token de cancelacion
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
          Config["ruta"],
          new FirebaseStorageOptions{
              AuthTokenAsyncFactory =()=> Task.FromResult(a.FirebaseToken),
              ThrowOnCancel=true
            }
          )
          .Child(Config[CarpetaDestino])
          .Child(NombreArchivo)
          .PutAsync(StreamArchivo,cancellation.Token);

        urlImagen = await task;

      }
      catch (Exception)
      {

        urlImagen = ""; 
      }

        return urlImagen;
    
    }

    public async Task<bool> EliminarStorage(string CarpetaDestino, string NombreArchivo)
    {
      try
      {
        //Copiado de CorreoService
        IQueryable<Configuracion> query = await _repositorio.Consultar(c => c.TxtRecurso.Equals("FireBase_Storage"));
        Dictionary<string, string> Config = query.ToDictionary(keySelector: c => c.TxtPropiedad, elementSelector: c => c.TxtValor);
        //-------------------------

        var auth = new FirebaseAuthProvider(new FirebaseConfig(Config["api_key"]));
        var a = await auth.SignInWithEmailAndPasswordAsync(Config["email"], Config["clave"]);
        //token de cancelacion
        var cancellation = new CancellationTokenSource();

        var task = new FirebaseStorage(
          Config["rura"],
          new FirebaseStorageOptions
          {
            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
            ThrowOnCancel = true
          }
          )
          .Child(Config[CarpetaDestino])
          .Child(Config[NombreArchivo])
          .DeleteAsync();

        await task;
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }


  }
}
