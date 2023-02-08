
var builder = WebApplication.CreateBuilder(args);

// Add Mvc to project
builder.Services.AddMvc();

// Add Runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(name: "dafualt", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
