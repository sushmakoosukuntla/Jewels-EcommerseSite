using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCatalogApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogApi
{
    //This is the super important class in the entire project
    //In this class we should provide the connection strings to the database
    //This is the class that is going to inject the connection strings to CatalogContext
    //CatalogContext.cs and CatalogSeed.cs comes together in the startup
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//Configuring MicroServices.
        {
            services.AddControllers();

            //Here is where we bring in the connection strings and inject in to the database.
            /*We created a variable called connectionString and Read my ConnectionString from Configuration file.*/
            var connectionString = Configuration["ConnectionString"];

            //Now we have to inject this connection to the DB context.
            /*We are saying to services that, Hey services AddDbContext to my project, Which means, my project
            requires you to set up a DB context CatalogContext*/
            //CatalogContext is a type of DBContext
            /*options => options.UseSqlServer(), we are telling what kind of database we are using,
            which is sql server here and we are providing connection strings as a parameter. This is how we 
            officialy injected the database */
            services.AddDbContext<CatalogContext>(options => options.UseSqlServer(connectionString));

            //Now second part is we need to seed the data. We are not seeding the data here.
            //Right after database setup, seeding the data is not a good process. 
            //so we are seeding the data in program.cs file.
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
