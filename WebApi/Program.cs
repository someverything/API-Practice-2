using Business.DependencyResolver;
using Core.DependenyResolver;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using WebApi.Middlewears;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBusinessService();
builder.Services.AddCoreService();

var supportedCultures = new[]
{
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU"),
        // Add other cultures as needed
    };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


FluentValidationMvcExtensions.AddFluentValidation(builder.Services.AddControllersWithViews(), x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    //x.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("az");
});

builder.Services.AddTransient<LocalizationMiddlewear>();
builder.Services.AddTransient<GlobalHandlerMiddlewear>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LocalizationMiddlewear>();
app.UseMiddleware<GlobalHandlerMiddlewear>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
