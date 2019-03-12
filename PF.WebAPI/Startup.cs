using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PF.WebAPI.ExceptionHandling;
using PF.WebAPI.ServiceRegistration;
using PF.WebAPI.Services.Filtering;
using PF.WebAPI.Services.StringParsing;
using Swashbuckle.AspNetCore.Swagger;

namespace PF.WebAPI
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
            services.AddTransient<IStringParser, StringParser>();
            services.AddSingleton<IPrimeGeneratorInitialiser, PrimeGeneratorConfigurationInitialiser>();
            services.AddSingleton<IPrimesGenerator, SieveOfEratosthenes>();
            services.AddSingleton<IPrimeTester, BruteForcePrimeTester>();

            services.AddFromConfigurationFile(Configuration.GetSection("Services"));

            services.AddMvc(options =>
            {
                // provide nicely formatted json in event of error
                options.Filters.Add(typeof(ExceptionFilterAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2); ;

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("doc", new Info
                {
                    Version = Assembly.GetEntryAssembly().GetName().Version.ToString(),
                    Title = "Technical Test",
                    Description = "Filter and Sort Numbers ",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var path = "TechTest";
            var helpRoot = "help";
            var swaggerRoot = "swagger/doc/swagger.json";

            if (!string.IsNullOrWhiteSpace(path))
            {
                path = path.StartsWith("/") ? path : "/" + path;
                app.UsePathBase(path);

                swaggerRoot = $"{path}/{swaggerRoot}";
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = helpRoot;
                c.SwaggerEndpoint(swaggerRoot, "Technical Test");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            var primesGenerator = app.ApplicationServices.GetService<IPrimesGenerator>();
            // generate primes 
            primesGenerator.Initialise();
        }
    }
}
