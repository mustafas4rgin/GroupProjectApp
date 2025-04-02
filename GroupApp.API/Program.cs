using System.Text;
using FluentValidation;
using GroupApp.API;
using GroupApp.Core.Concrete;
using GroupApp.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using AutoMapper;
using GroupApp.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // React uygulamanın adresi
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials(); // Eğer cookie ile authentication yapıyorsan bunu ekle
        });
});

var key = Encoding.UTF8.GetBytes("HayatimdakiEnGuvenliAnahtarBuOlsaGerek230723");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "groupapp.com",
            ValidAudience = "groupapp.com",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// AutoMapper'ı profillerle birlikte kaydediyoruz
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Profil tanımları burada aranacak

builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddBusinessService();

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
