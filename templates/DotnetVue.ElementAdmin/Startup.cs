using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotnetVue.ElementAdmin.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DotnetVue.ElementAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetVue.ElementAdmin", Version = "v1" });
            });

            ConfigureCors(services);
            services.UseJwtBearerAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetVue.ElementAdmin v1"));
            }

            app.UseCors(DefaultCorsPolicyName);
            
            #region 设置静态文件

            app.UseStaticFiles();

            string filesDirectory = Configuration.GetSection("UploadFileDirectory").Value;
            if (string.IsNullOrWhiteSpace(filesDirectory))
            {
                filesDirectory = Directory.GetCurrentDirectory();
            }
            string publiclyFilesPath = Path.Combine(filesDirectory, @"files");
            if (!Directory.Exists(publiclyFilesPath))
            {
                Directory.CreateDirectory(publiclyFilesPath);
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(publiclyFilesPath),
                RequestPath = new PathString("/files"),
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
                }
            });
            
            #endregion 设置静态文件
            
            app.UseFileServer();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        private const string DefaultCorsPolicyName = "Default";

        private void ConfigureCors(IServiceCollection services)
        {
            IWebHostEnvironment env = services.BuildServiceProvider().GetService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(DefaultCorsPolicyName, builder =>
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
                });
            }
        }
    }
}
