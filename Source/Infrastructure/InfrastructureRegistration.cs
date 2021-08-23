using Application.Contracts.Infrastructure;
using Application.Contracts.Repositories;
using Application.Contracts.Services.GoogleGeocodingService;
using Application.Contracts.Services.Identity;
using Application.Models.Authentication;
using Application.Models.Email;
using Infrastructure.Mail;
using Infrastructure.Models;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;
using Infrastructure.Services.GoogleGeocodingService;
using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistence;
using Persistence.Repository;
using System;
using System.Text;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

            //Start Identity
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<VehicleTrackingSystemDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("VehicleTrackingSystemConnectionString"),
                b => b.MigrationsAssembly(typeof(VehicleTrackingSystemDbContext).Assembly.FullName)));

            services.AddSingleton<ISqlConnectionFactory>(x =>
                new SqlConnectionFactory(configuration.GetConnectionString("VehicleTrackingSystemConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<VehicleTrackingSystemDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthenticationService, AuthenticationService>();

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };

                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("401 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("403 Not authorized");
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            //End Identity

            //start persistance
            services.AddDbContext<VehicleTrackingSystemDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("VehicleTrackingSystemIdentityConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITrackingDeviceRepository, TrackingDeviceRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IGeocodingService, GeocodingService>();

            return services;
        }
    }
}