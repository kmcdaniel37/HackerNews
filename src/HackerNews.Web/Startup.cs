using HackerNews.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HackerNews.Web
{
    public class Startup
    {
        private const string HackerNewSApiUri = "HackerNewSApiUri";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(CreateConfigurationRoot);

            services.AddSingleton(CreateGetNewestStoriesProxy);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
        
        private static IConfigurationRoot CreateConfigurationRoot(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetRequiredService<IHostingEnvironment>();
            var retrieval = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();
            return retrieval;
        }

        private static IGetNewestStoriesProxy CreateGetNewestStoriesProxy(IServiceProvider sp)
        {
            var baseUri = GetHackerNewSApiUri(sp,HackerNewSApiUri);
            return new GetNewestStoriesProxy(baseUri);
        }

        public static string GetHackerNewSApiUri(IServiceProvider sp, string sectionKey)
        {
            var configurationClient = sp.GetRequiredService<IConfigurationRoot>();
            var retval = configurationClient.GetSection(sectionKey).Value;
            return retval;
        }

    }
}
