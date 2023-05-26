using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services;
using SIPI_CRM_System.Services.LoginPageRep;
using SIPI_CRM_System.Services.MainPageRep;
using SIPI_CRM_System.Services.StockPage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CRMdbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CRMdb")));

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<ILoginPageRepository, DataBaseLoginPageRepository>();
builder.Services.AddScoped<IMainPageRepository, DataBaseMainPageRepository>();
builder.Services.AddScoped<IStockPageRepository, DataBaseStockPageRepository>();
builder.Services.AddHostedService<ProductFitnessControlService>();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Configure cookies params
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Configure session params
    options.Cookie.Name = "MyApp.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(3600);
    options.Cookie.IsEssential = true;
});
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CRMdbContext>();
    context.Database.EnsureCreated();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

