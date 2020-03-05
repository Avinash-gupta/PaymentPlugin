using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalPaymentSolutions.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GlobalPaymentSolutions.Options;
using Microsoft.OpenApi.Models;
namespace GlobalPaymentSolutions
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
            IConfigurationSection sec = Configuration.GetSection("appSettings");
            services.Configure<appSettings>(sec);
            
            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Global Payment", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var SwaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(SwaggerOptions);
            app.UseSwagger(option =>
            {
                option.RouteTemplate = SwaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option=>
            {
                option.SwaggerEndpoint(SwaggerOptions.UIEndpoint, SwaggerOptions.Description);
            });
            app.UseHttpMethodOverride();
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



