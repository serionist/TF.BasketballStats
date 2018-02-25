using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TF.BasketballStats.Database;

namespace TF.BasketballStats
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
            services.AddLogging(l => l.AddConfiguration(Configuration).AddAzureWebAppDiagnostics());
            services.AddMvc();
            services.AddDbContext<DatabaseContext>(e => e.UseSqlite(Configuration.GetConnectionString("main")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "TF Basketball api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(e => e.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }

            using (var scop = app.ApplicationServices.CreateScope())
            {
                
                scop.ServiceProvider.GetService<DatabaseContext>().Database.EnsureCreated();
                scop.ServiceProvider.GetService<DatabaseContext>().Database.Migrate();
            }
            app.UseSwagger();


            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404 &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/dist/index.html";
                    await next();
                }
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TF Basketball API v1");
            });
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
