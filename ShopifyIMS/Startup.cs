using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization;
using ShopifyIMS.Dal;
using ShopifyIMS.Models;
using ShopifyIMS.Services;

namespace ShopifyIMS
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

            services.Configure<InventoryManagementSystemDBContext>(options =>
            {
                options.ConnectionString = Environment.GetEnvironmentVariable("ShopifyIMSDatabaseConnString");
                options.DatabaseName = Configuration.GetSection("InventoryDatabase:DatabaseName").Value;
                options.CollectionName = Configuration.GetSection("InventoryDatabase:CollectionName").Value;
            });

            BsonClassMap.RegisterClassMap<InventoryItem>(cm =>
               cm.AutoMap()
           );

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<InventoryRepository>();
            services.AddSingleton<InventoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
