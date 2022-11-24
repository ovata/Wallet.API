using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Data;
using Wallet.Data.Entities;
using Wallet.Data.Services;
using Wallet.Service.DTOs;

namespace Wallet.Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TokenSettings>(config.GetSection("JwtSettings"));
            var secret = config.GetValue<string>("JwtSettings:Secret");
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(configureOptions: b =>
            {
                b.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                b.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                b.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }). AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WalletAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
             });

            services.AddDbContext<ApplicationContext>(opt => opt.
               UseSqlServer(config.GetConnectionString("Wallet")));

            services.AddTransient(typeof(GenericDataRepository<>));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Wallet.Service"));
            services.AddMediatR(AppDomain.CurrentDomain.Load("Wallet.API"));


            return services;
        }
    }
}
