using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
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
            services.AddDbContext<DataContext> (opt => {

                  opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));  
            });
            //clientlarda CORS hatası almamak için aşağıdaki servisi ekliyoruz.
            services.AddCors(opt => {
                opt.AddPolicy("CorsePolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().
                    WithOrigins("http://localhost:3000");//react client portu
                });
            });

            //mediator servisini ekledik
              services.AddMediatR(typeof(List.Handler).Assembly);
            //  services.AddMediatR(typeof(Details.Handler).Assembly);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

// https i kapat
           app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

// client'lardan cors erişim hatası almamak için ekliyoruz.
// CorsePolicy configureServiste verdiğimiz isimle aynı olmalı!
app.UseCors("CorsePolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
