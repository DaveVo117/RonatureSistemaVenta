using Ronature.AplicacionWeb.Utilidades.Automapper; //se importa despues de configurar el perfil de mapeo
using Ronature.IOC;

//referencias de librerias pdf
using Ronature.AplicacionWeb.Utilidades.Extensiones;
using DinkToPdf;
using DinkToPdf.Contracts;

using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ConfiguracionCookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
//Agregar builder--------------------------------------------------------
builder.Services.InyectarDependencia(builder.Configuration);
//Agregar configuracion de mapeo (perfil)
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//Configuracion de librer�a PDF
var context = new CustomAssemblyLoadContext();
context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Utilidades/LibreriaPDF/libwkhtmltox.dll"));
builder.Services.AddSingleton(typeof(IConverter),new SynchronizedConverter(new PdfTools()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//Cookies
app.UseAuthentication();

app.UseAuthorization();

//Ruta deInicio de Aplicaci�n, se cambia de home/index a Acceso/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
