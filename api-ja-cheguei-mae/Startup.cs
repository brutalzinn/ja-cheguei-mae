
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
     
            services.AddSwaggerGen(c =>
            {
     
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Api Mãe cheguei",
                    Version = "v1",
                    Description = "Api que desenvolvi para mandar mensagem automática para minha mãe quando eu chegar no trabalho.",
                    Contact = new OpenApiContact
                    {
                        Name = "Roberto Caneiro Paes",
                        Url = new System.Uri("https://github.com/brutalzinn")
                    }

                }); ;

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

        //testando injeção de dependência com aspcore 

        services.AddSingleton<IMensagemService, MensagemService>();

        services.AddControllers();

  
            //services.AddDbContext<api_target_desafioContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("api_target_desafioContext")));

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
   


