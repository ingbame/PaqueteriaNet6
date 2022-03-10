using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Paqueteria.Rastreo.Web.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
builder.Services.AddDbContext<PaqueteriaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PackagesContext")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Servicio de documentacion automatica de las APIs
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Api de prueba para la Paquetería SCRUD Simple",
        Version = "v1",
        Description = "Esta Api es en relación prueba, sobre la junta del  Jueves con Vicente Torres",
        Contact = new OpenApiContact
        {
            Name = "ISC. Baruch Medina",
            Email = "ingbame@gmail.com",
            Url = new System.Uri("https://github.com/ingbame/PaqueteriaNet6")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","API de Muestra simple");
        c.RoutePrefix = string.Empty;
    });
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
