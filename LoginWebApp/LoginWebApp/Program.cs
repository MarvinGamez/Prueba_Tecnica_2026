using LoginWebApp.BLL;
using LoginWebApp.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ?? NECESARIO PARA SESSION
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseExceptionHandler("/Home/Error");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();
