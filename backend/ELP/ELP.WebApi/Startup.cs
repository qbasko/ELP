using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ELP.Model;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ELP.Service;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ELP.Model.Entities;
using ELP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ELP.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(opt =>
            {
                var resolver = opt.SerializerSettings.ContractResolver;
                if (resolver != null)
                {
                    var res = resolver as DefaultContractResolver;
                    res.NamingStrategy = null;
                }
            }); 

            //services.AddMvcCore().AddJsonFormatters(j => j.Formatting = Formatting.Indented);



            var connectionString = Configuration["connectionStrings:ELPdb"];
            services.AddDbContext<ELPContext>(o => o.UseSqlServer(connectionString));

            var builder = new ContainerBuilder();
            builder.RegisterModule<ModelModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<WebApiModule>();
            builder.Populate(services);

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            AutoMapper.Mapper.Initialize(cfg =>
            {                                    //for custom mapping
                 //.ForMember(dest => dest.Username, opt => opt.MapFrom(src => "UserName: " + src.Username));

                cfg.CreateMap<Event, EventDto>();
            });

            app.UseCors(config => config.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            

            app.UseMvc();

        }

    }
}
