using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();  // <--- agregar

// Auth por cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Account/Login";
        opt.AccessDeniedPath = "/Account/Login";
    });

// HttpClient nombrado con BaseUrl del appsettings
builder.Services.AddHttpClient("ConversorApi", (sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["ApiBaseUrl"]?.TrimEnd('/') + "/";
    http.BaseAddress = new Uri(baseUrl);
});

// Nuestro servicio REST
builder.Services.AddScoped<ConUni_Rest_WebClient.Services.ConversorApi>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Conversor}/{action=Index}/{id?}");

app.Run();
