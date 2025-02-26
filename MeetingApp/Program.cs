var builder = WebApplication.CreateBuilder(args);

// mvc yapısı
builder.Services.AddControllersWithViews(); 

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

// controller/action/id?
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
