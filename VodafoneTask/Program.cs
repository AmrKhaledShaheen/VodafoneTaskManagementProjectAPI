using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DataContext;
using Services.AccountManagementService;
using Infrastructure.Mapping;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.TaskService;
using Services.SubscriptionService;
using Microsoft.OpenApi.Models;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Services.BackgroundServiceForEmail;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContextTask>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmrConnectionString")));
builder.Services.AddIdentity<VodafoneUser, IdentityRole>()
    .AddEntityFrameworkStores<DbContextTask>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>) );
builder.Services.AddScoped<IUnitOfWork,GenericUnitOfWork>();
builder.Services.AddScoped<IAccountManagmentService, AccountManagmentService>();
builder.Services.AddAutoMapper(typeof(AccountManagementMap));
builder.Services.AddScoped<ITaskService,TaskService>();
builder.Services.AddAutoMapper(typeof(TaskMapping));
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddAutoMapper(typeof(SubscriptionMapping));
/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/AccountManagement/Index";
});
builder.Services.AddAuthorization();*/


builder.Services.AddSingleton<EmailService>();
builder.Services.AddHostedService<BackgroundServiceForEmail>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "An example API for Swagger integration"
    });
});

var app = builder.Build();

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger will be available at the root URL
    });
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCookiePolicy();

app.UseAuthentication(); // Authentication middleware should be placed before authorization

app.UseAuthorization(); // Authorization middleware should be placed after authentication

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AccountManagement}/{action=Index}/{id?}");

app.Run();
