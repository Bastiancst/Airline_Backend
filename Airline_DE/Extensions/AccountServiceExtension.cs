using Airline_DE.DbContext;
using Airline_DE.Interfaces;
using Airline_DE.Models.User;
using Airline_DE.Services.AccountServices;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace Airline_DE.Extensions
{
    public static class AccountServiceExtension
    {
        public static void AddAccountServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(ConnectionSettings.ConnectionString));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters = null;

            }).AddEntityFrameworkStores<Context>()
              .AddDefaultTokenProviders();
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        o.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            ValidIssuer = JWTSettings.Issuer,
                            ValidAudience = JWTSettings.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Key))
                        };
                        
                        return Task.CompletedTask;
                    },
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
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("Not authorized"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("Resource not allowed"));
                        return context.Response.WriteAsync(result);
                    }
                };
            });
        }
    }
}
