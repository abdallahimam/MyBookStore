using BookStore.Data;
using BookStore.Models;
using BookStore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStore.Helpers;
using BookStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context with connection string from the appsettong.json file
builder.Services.AddDbContext<BookStoreDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add Mvc to project
builder.Services.AddMvc();

// Add Runtime compilation
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookStoreDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = true;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(option => 
{
    option.TokenLifespan = TimeSpan.FromMinutes(5);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = builder.Configuration.GetSection("Application").GetValue<string>("LoginPath");
});

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

#if DEBUG
builder.Services.AddRazorPages().AddViewOptions(option => 
{
    option.HtmlHelperOptions.ClientValidationEnabled = false;
});
#endif

// Add DI
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<ContactInfoConfig>("ContactInfoConfig1", builder.Configuration.GetSection("ContactInfoConfig1"));
builder.Services.Configure<ContactInfoConfig>("ContactInfoConfig2", builder.Configuration.GetSection("ContactInfoConfig2"));
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));


var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "dafualt", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
