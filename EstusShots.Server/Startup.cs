using AutoMapper;
using EstusShots.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EstusShots.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private bool IsDevelopment { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EstusShotsContext>(
                opt =>
                {
                    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    opt.UseSqlite(Configuration.GetConnectionString("Sqlite"));
                });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                if (IsDevelopment)
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                }
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Estus Shots API", Version = "v1" });
            });

            // Register business logic services 
            services.AddScoped<SeasonsService>();
            services.AddScoped<EpisodesService>();
            services.AddScoped<PlayersService>();
            services.AddScoped<DrinksService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            IsDevelopment = env.IsDevelopment();
            if (IsDevelopment)
                app.UseDeveloperExceptionPage();
            
            // Do not Redirect for now. Breaks local tests.
            // app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Estus Shots API V1");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}