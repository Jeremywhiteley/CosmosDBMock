// ******************************
// Axis Project
// @__harveyt__
// ******************************
using LibraryApi.Services;
using Library.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace LibraryApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public static string PATH { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CosmosLibraryApi", Version = "v1" });
            });

            // set the Emulator or Azure account
            var cosmosDBSettings = Configuration.GetSection("CosmosDbEmulator").Get<CosmosDBSettings>();

            // cosmos db services
            services.AddSingleton<ICosmosService<Book>>(DatabaseInitializer.Initialize<Book>(cosmosDBSettings).GetAwaiter().GetResult());
            services.AddSingleton<ICosmosService<Student>>(DatabaseInitializer.Initialize<Student>(cosmosDBSettings).GetAwaiter().GetResult());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CosmosLibraryApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            // application pah for static files
            PATH = env.ContentRootPath;
        }
    }
}
