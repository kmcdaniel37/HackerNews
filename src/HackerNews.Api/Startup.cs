using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Api.Stories.Mapper;
using HackerNews.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HackerNews.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile( new StoriesProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));

            services.AddSingleton<IStoryRepository, StoryRepository>();
            services.AddMemoryCache();
            services.AddSingleton(new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(1)));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
