﻿using Microsoft.EntityFrameworkCore;
using SIPI_CRM_System.Models;
using SIPI_CRM_System.Services.LoginPageRep;
using SIPI_CRM_System.Services.MainPageRep;
using SIPI_CRM_System.Services.StockPageRep;
using SIPI_CRM_System.Services.UserPageRep;
using SIPI_CRM_System.Services.MenuPageRep;
using SIPI_CRM_System.Services.DishEditPageRep;
using SIPI_CRM_System.Services.OrderEditPageRep;
using SIPI_CRM_System.Services.StockPage;
using SIPI_CRM_System.Services.AdminPanelRep;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CRMdbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("CRMdb")));

builder.Services.AddRazorPages();

builder.Services.AddScoped<ILoginPageRepository, DataBaseLoginPageRepository>();
builder.Services.AddScoped<IMainPageRepository, DataBaseMainPageRepository>();
builder.Services.AddScoped<IStockPageRepository, DataBaseStockPageRepository>();
builder.Services.AddScoped<IUserPageRepository, DataBaseUserPageRepository>();
builder.Services.AddScoped<IMenuPageRepository, DataBaseMenuPageRepository>();
builder.Services.AddScoped<IDishEditPageRepository, DataBaseDishEditPageRepository>();
builder.Services.AddScoped<IOrderEditPageRepository, DataBaseOrderEditPageRepository>();
builder.Services.AddScoped<IAdminPanelRepository, DataBaseAdminPanelRepository>();

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

    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return;
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

