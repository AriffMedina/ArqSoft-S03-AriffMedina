using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Ruta del archivo JSON — se guarda en la carpeta "data" del proyecto

var jsonPath = Path.Combine(

builder.Environment.ContentRootPath, "data", "items.json");

// Registrar el repositorio JSON como implementación de IItemRepository

builder.Services.AddSingleton<IItemRepository>(

new JsonItemRepository(jsonPath)
);

// Registrar el servicio de Application

var jsonPathReviews = Path.Combine(
    builder.Environment.ContentRootPath, "Data", "reviews.json");

builder.Services.AddSingleton<IReviewRepository>(
    new JsonReviewRepository(jsonPathReviews));

builder.Services.AddScoped<ReviewService>();


builder.Services.AddScoped<ItemService>();

var jsonPathUsuarios = Path.Combine(
    builder.Environment.ContentRootPath, "Data", "users.json");

builder.Services.AddSingleton<IUserRepository>(
    new JsonUserRepository(jsonPathUsuarios));

builder.Services.AddScoped<UserService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // sesión expira en 30 min
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())

{

    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.app.UseHsts();

}

app.UseHttpsRedirection(); app.UseStaticFiles(); app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(

    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}")

    .WithStaticAssets();


app.Run();