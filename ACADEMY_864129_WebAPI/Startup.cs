using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACADEMY_864129_WebAPI.Models;
using ACADEMY_864129_WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace ACADEMY_864129_WebAPI
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
            services.AddControllers().AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            IConfigurationSection appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);
            services.AddScoped<ITableStorage, TableStorage>();
            services.AddScoped<ICosmosDataBase, CosmosDataBase>();
            services.AddScoped<IConfigIoTHub, ConfigIoTHub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
           

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            NLog.LogManager.LoadConfiguration("nlog.config");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
