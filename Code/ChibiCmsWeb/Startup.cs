using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ChibiCmsWeb.Helpers;
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
            var contentManager = new ContentManager(Path.Combine(HostingEnvironment.WebRootPath, @"contents"));
            //set the paht of the content to a env variable
            try
            {
                Environment.SetEnvironmentVariable(Configuration["ContentPathEnvVar"], Path.Combine(HostingEnvironment.WebRootPath, @"contents"), EnvironmentVariableTarget.Machine);
            }
            catch (Exception)
            {
                //not enough previldge do nothing
            }
            
            services.AddSingleton<ContentManager>(contentManager);
            var scripts = new UpdateScripts(Configuration, Path.Combine(HostingEnvironment.WebRootPath, @"updatescripts"));
            services.AddSingleton<UpdateScripts>(scripts);

            //set view path for template
            if (!string.IsNullOrEmpty(Configuration["TemplatePath"]))
            {
                services.Configure<RazorViewEngineOptions>(o =>
                {
                    o.ViewLocationFormats.Clear();
                    // The old locations are / Views /{ 1}/{ 0}.cshtml and / Views / Shared /{ 0}.cshtml where { 1} is the controller and { 0} is the name of the View
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
                    template: "{controller=index}/{action=index}/{*path}");
                routes.MapRoute(
                    name: "index",
                    template: "index/{*path}",
                    defaults: new { controller = "index", action = "index" });
                routes.MapRoute(
                    name: "content",
                    template: "contents/{*path}",
                    defaults: new { controller = "contents", action = "getOneContent" });
            });
        }
    }
}
