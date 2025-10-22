using ConUni_Soap_Dotnet_CliWeb_G04.Services; // <-- tu wrapper SOAP
// using Microsoft.AspNetCore.Mvc;  // (opcional si usas filtros, etc.)

var builder = WebApplication.CreateBuilder(args);

// MVC con vistas
builder.Services.AddControllersWithViews();

// Registra tu wrapper que llama al servicio SOAP
builder.Services.AddSingleton<ConversorSoapClient>();

var app = builder.Build();

// Manejo de errores y seguridad básica
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// app.UseAuthorization(); // (déjalo si luego agregas auth)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conversor}/{action=Index}/{id?}"); // ? vamos directo a tu conversor

app.Run();
