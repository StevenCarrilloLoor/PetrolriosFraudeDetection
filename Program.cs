using Microsoft.EntityFrameworkCore;
using PetrolriosFraudeDetection.Data;
using PetrolriosFraudeDetection.Models.Services;
using PetrolriosFraudeDetection.Validators;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de servicios
builder.Services.AddScoped<CodigoEstacionValidator>();
builder.Services.AddScoped<NumeroFacturaValidator>();
builder.Services.AddScoped<MotorDeteccion>();

// Configuración de MVC 
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();