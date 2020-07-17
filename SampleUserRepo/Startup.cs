using System.Text.Json.Serialization;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using SampleUserRepo.Context;
using SampleUserRepo.services;
using SampleUserRepo.Interfaces;
//using SampleUserRepo.services;

namespace SampleUserRepo
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IWebHostEnvironment HostingEnvironment { get; }


        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            var builtConfig = builder.Build();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }


            this.HostingEnvironment = env;
            this.Configuration = builder.Build();
        }


        public IContainer ApplicationContainer { get; private set; }


        public IConfiguration Configuration { get; private set; }


        public ILifetimeScope AutofacContainer { get; private set; }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Url rewrite to HTTPS.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // Need this for get default correlation Id
          

            app.UseEndpoints(endpoints =>
            {
                // Mapping of endpoints goes here:
                endpoints.MapControllers();
            });
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://example.com",
                                                          "http://www.contoso.com",
                                                          "http://localhost:4200")
                                                   .AllowAnyHeader()
                                                  .AllowAnyMethod();
                                  });
            });
            services.AddOptions();
            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());

                // To support XML input and output
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
            })
            .SetCompatibilityVersion(CompatibilityVersion.Latest);


            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddDbContextPool<crsuserauthdeContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(this.Configuration.GetValue<string>("ConnectionStrings:SampleUserRepo"));
                optionsBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddSwaggerGen();
            // Initiate custom DbContext
            /*services.InitalizeCustomDbContext();*/

            /*services.AddDbContextPool<SampleAuditContext>(options => options.UseSqlServer(this.Configuration.GetValue<string>("ConnectionStrings:SampleAudit")));*/

            var builder = new ContainerBuilder();
            builder.Populate(services);

            this.ApplicationContainer = builder.Build();
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {

            //builder.RegisterType<TimeZonesService>().As<ITimeZonesService>()
            //  .InstancePerLifetimeScope();
            builder.RegisterType<CountryPreferenceService>().As<ICountryPreferenceService>().InstancePerLifetimeScope();
            builder.RegisterType<UserPreferenceService>().As<IUserPreferenceService>().InstancePerLifetimeScope();

            builder.RegisterType<crsuserauthdeContext>()
              .AsSelf()
              .InstancePerLifetimeScope()
              .UsingConstructor(typeof(string))
              .WithParameters(new List<Parameter>
              {
                    new NamedParameter("connectionString", this.Configuration.GetValue<string>("ConnectionStrings:SampleUserRepo"))

              });



            builder.RegisterType<SqlDbRepository>().As<IDbRepository>().InstancePerLifetimeScope().UsingConstructor(typeof(crsuserauthdeContext))
             .WithParameters(new List<Parameter>
             {
                    new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(crsuserauthdeContext),
                    (pi, ctx) => ctx.Resolve<crsuserauthdeContext>())
             }); ;


        }


    }
}
