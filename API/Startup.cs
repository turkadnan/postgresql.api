using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mapper;
using AutoMapper;
using Data.Layer.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
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

            services.AddDbContext<Data.Layer.Data.ApplicationDbContext>(
                options => options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")
                    )
                );

            //services.AddScoped<IDataRepository, DataRepository>();

            //DTO's Mapping
            services.AddAutoMapper(typeof(ApiMappings));
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("IPSOSAPISpect",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "IPSOS API Spect",
                        Version = "1"
                    });
            }
            );

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/IPSOSAPISpect/swagger.json", "IPSOS API");
                //Default olarak swagger açýlmasý için yapýldý
                options.RoutePrefix = "";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
