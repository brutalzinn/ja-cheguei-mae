
using api_ja_cheguei_mae.Middlewares;
using api_ja_cheguei_mae.PostgreeSQL;
using api_ja_cheguei_mae.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
        /// SWAGGER IMPLEMENTATION HERE <-- #CODE-SWAGGER
            services.AddSwaggerGen(c =>
            {  
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Mãe cheguei",
                    Version = "v1",
                    Description = "Api que desenvolvi para mandar mensagem automática para minha mãe quando eu chegar no trabalho.",
                    Contact = new OpenApiContact
                    {
                        Name = "Roberto Carneiro Paes",
                        Url = new System.Uri("https://github.com/brutalzinn")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath,true);
            });
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            services.AddHttpContextAccessor();
            services.AddDbContext<DatabaseContexto>(options => options.UseNpgsql(
            Configuration.GetConnectionString("DefaultConnetion")));
            services.AddSingleton<IMensagemService, MensagemService>();
            services.AddControllers();

    }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc - Api Target Desafio v1"));
            }
        //app.Map("/user", UserMiddleware);


            app.UseRouting();
            app.UseMiddleware<AuthMiddleware>();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


    }
    }
   


