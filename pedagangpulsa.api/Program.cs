using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using pedagangpulsa.api.Context;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using pedagangpulsa.api.Service;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using pedagangpulsa.api.Helpter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddFluentValidationRulesToSwagger();


builder.Services.AddScoped<dbPedagangPulsaContext>();

// MYSQL DB Conn
string koneksiDb = configuration.GetConnectionString("Default");
services.AddDbContext<dbPedagangPulsaContext>(o => o.UseMySql(koneksiDb, new MySqlServerVersion(new Version(8, 0, 23))));

services.AddAutoMapper(typeof(MappingProfiles));

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]
                ))
        };
    });


// Service
services.AddTransient<KonterService, KonterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
