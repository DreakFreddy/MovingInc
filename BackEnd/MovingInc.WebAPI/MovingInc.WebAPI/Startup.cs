// ------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Tech&Solve">
//     COPYRIGHT(C) 2020, Tech&Solve
// </copyright>
// <author>Freddy Zabala</author>
// <email>freddyzabala@live.com</email>
// <date>31/10/2020</date>
// <summary>Implementa el StartUp del API/summary>
// ------------------------------------------------------------------------------------------------

namespace MovingInc.WebAPI
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.OpenApi.Models;
    using MovingInc.Repositorio;
    using System;

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
            services.AddControllers();
            services.AddDbContext<MudanzasContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            AddSwagger(services);
            services.AddTransient<Negocio.Interfaces.IMudanzasNegocio, Negocio.Clases.MudanzasNegocio>();
            services.AddTransient<Repositorio.Interfaces.IMudanzasRepositorio, Repositorio.Clases.MudanzasRepositorio>();
            services.AddCors();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Moving Inc - WebAPI {groupName}",
                    Version = groupName,
                    Description = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Developed by Freddy Zabala",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Moving Inc WebAPI");
            });

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
