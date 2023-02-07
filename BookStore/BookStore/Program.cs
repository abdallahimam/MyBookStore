using Microsoft.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Mvc to project
builder.Services.AddMvc();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(name: "dafualt", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
