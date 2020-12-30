using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DotnetVue.ElementAdmin.Authentication
{
    public static class AuthenticationExtensions
    {

        /// <summary>
        /// 使用cookie验证
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void UseCookieAuthentication(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
                {
                    options.AddPolicy("admin", policy => policy.Requirements.Add(new PermissionRequirement("admin")));
                })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, c =>
                {
                    c.LoginPath = new PathString("/user/login");
                    c.Events.OnRedirectToLogin = (context) =>
                    {
                        // if (context.Request.IsAjax())
                        // {
                        //     context.HttpContext.Response.StatusCode = 401;
                        // }
                        // else
                        // {
                        //     context.Response.Redirect(context.RedirectUri);
                        // }
                            
                        context.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
              services.AddTransient<IAuthorizationHandler, PermissionHandler>();
        }
        
        public static void UseJwtBearerAuthentication(this IServiceCollection services)
        {
            IConfiguration configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy => 
                    policy.Requirements.Add(new PermissionRequirement("admin")));
            }).AddAuthentication(options =>
            {                    
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                (jwtBearerOptions) =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:SecurityKey").Value)),//秘钥
                        ValidateIssuer = true,
                        ValidIssuer = configuration.GetSection("Jwt:Issuer").Value,
                        ValidateAudience = true,
                        ValidAudience = configuration.GetSection("Jwt:Audience").Value,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5),
                        RequireExpirationTime = true
                    };
                }
            );
            services.AddTransient<IAuthorizationHandler, PermissionHandler>();
        }
    }
}
