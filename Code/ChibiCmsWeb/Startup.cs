using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jushen.ChibiCms.ChibiContent;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChibiCmsWeb
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public IHostingEnvironment HostingEnvironment { get; private set; }

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var contentManager = new ContentManager(HostingEnvironment.WebRootPath+@"\contents");
            services.AddSingleton<ContentManager>(contentManager);


            //set view path for template
            if (!string.IsNullOrEmpty(Configuration["TemplatePath"]))
            {
                services.Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                    o.ViewLocationFormats.Add(Configuration["TemplatePath"] +"/{1}/{0}" + RazorViewEngine.ViewExtension);
                    o.ViewLocationFormats.Add(Configuration["TemplatePath"] +"/Shared/{0}" + RazorViewEngine.ViewExtension);
                });
            }
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "content",
                    template: "contents/{*path}",
                    defaults: new { controller = "contents", action = "getOneContent" });
            });
        }
    }
}
