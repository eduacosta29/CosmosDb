using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;

namespace Cosmosdb.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cosmosdb.WebApi.Infrastructure.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Reflection;
    using System.IO;

    public class Startup
    {

        private readonly Configuration.Configuration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = new Configuration.Configuration();
            Configuration = configuration;
            configuration.Bind(_configuration);

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this._configuration);
            //services.AddSingleton<IDocumentClient>(new DocumentClient(new Uri(Configuration.GetSection("CosmosDb:URI").Value), Configuration.GetSection("CosmosDb:Key").Value));services.AddTransient<IRepository, Repository>();

            services.AddSingleton<IDocumentClient>(new DocumentClient(new Uri(this._configuration.CosmosDb.URI), this._configuration.CosmosDb.Key));
            services.AddTransient<IRepository, Repository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "My DAB API", Version = "V3.2.2" });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseMvc().UseMvcWithDefaultRoute();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
               
            });
        }
    }
}
