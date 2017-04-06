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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            _env = env;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                //if (!_env.IsProduction())
                //{
                //    opt.SslPort = 44388;
                //}
                //opt.Filters.Add(new RequireHttpsAttribute());

            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore;
            }); ;

            //services.AddMvcCore().AddJsonFormatters(j => j.Formatting = Formatting.Indented);
     

            var connectionString = Configuration["connectionStrings:ELPdb"];
            services.AddDbContext<ELPContext>(o => o.UseSqlServer(connectionString));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ELPContext>();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Cookies.ApplicationCookie.Events =
              new CookieAuthenticationEvents()
              {
                  OnRedirectToLogin = (ctx) =>
                  {
                      if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                      {
                          ctx.Response.StatusCode = 401;
                      }

                      return Task.CompletedTask;
                  },
                  OnRedirectToAccessDenied = (ctx) =>
                  {
                      if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                      {
                          ctx.Response.StatusCode = 403;
                      }

                      return Task.CompletedTask;
                  }
              };
            });

            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                var rdr = new QueryStringOrHeaderApiVersionReader("ver");
                rdr.HeaderNames.Add("X-MyCodeCamp-Version");
                cfg.ApiVersionReader = rdr;

            });

            services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("SuperUsers", p => p.RequireClaim("SuperUser", "True"));
            });

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

            app.UseIdentity();

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateLifetime = true
                }
            });

            app.UseMvc();

        }

    }
}
