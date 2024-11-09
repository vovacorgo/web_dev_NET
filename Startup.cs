using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;
using SupportCenter.Services;
using System.Net;
using Microsoft.Extensions.Localization;
using SupportCenter.Controllers;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace SupportCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Ensure Logs directory exists
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            // Configure Serilog to log to a file
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console() // Log to the console as well
                .WriteTo.File(Path.Combine(logDirectory, "requests.log"), rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application starting up...");
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register EmailSender service
            services.AddScoped<IEmailSender, EmailSender>();


            services.AddControllersWithViews()
                .AddDataAnnotationsLocalization()
                .AddViewLocalization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            //translations
            var supportedCultures = new[] { "en", "uk-UA" }; // Add other supported cultures here
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
            app.UseRequestLocalization(localizationOptions);


            app.UseHttpsRedirection();
            app.UseStaticFiles();




            // Register the request logging middleware
            app.UseMiddleware<RequestLoggingMiddleware>();



            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "fileUpload",
                    pattern: "file/upload",
                    defaults: new { controller = "File", action = "Upload" });


                endpoints.MapControllerRoute(
                    name: "fileIndex",
                    pattern: "files",
                    defaults: new { controller = "File", action = "ImagesView" });
            });
 

            // log temp flush on close
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            lifetime.ApplicationStopped.Register(Log.CloseAndFlush);
        }
    }

}
