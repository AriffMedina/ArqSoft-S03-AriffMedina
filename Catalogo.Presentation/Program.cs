using CatalogoApp.Application.Services;
using CatalogoApp.Domain.Interfaces;
using CatalogoApp.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

var jsonPath = Path.Combine(

builder.Environment.ContentRootPath, "data", "items.json");

builder.Services.AddSingleton<IItemRepository>(

new JsonItemRepository(jsonPath)
);

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
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())

{
    app.UseExceptionHandler("/Home/Error");

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