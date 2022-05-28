using api_ja_cheguei_mae.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using api_ja_cheguei_mae.Services.LoginService;
using api_ja_cheguei_mae.Services.JWTService;
using api_ja_cheguei_mae.Services.Redis;
using api_ja_cheguei_mae.Services;
using api_ja_cheguei_mae.Models;
using Services.DI;
using api_ja_cheguei_mae.Config;

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
          //  var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
          //  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
          //  c.IncludeXmlComments(xmlPath, true);
        });

        services.AddConfig(Configuration);
        services.Configure<GoogleConfig>(Configuration.GetSection("GoogleConfig"));
        services.Configure<Ambiente>(Configuration.GetSection("Ambiente"));
        services.AddHttpContextAccessor();
       
        services.AddSingleton<IMensagemService, MensagemService>();
        services.AddSingleton<IRedisService, RedisService>();
        services.AddTransient<IJWTService, JWTService>();
        services.AddTransient<IUsuarioService, UsuarioService>();
        services.AddControllers();

    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            //   app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc - Api Target Desafio v1"));
        }
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doc - Api Ja Cheguei Mae"));
        //app.Map("/user", UserMiddleware);
        var webSocketOptions = new WebSocketOptions()
        {
            KeepAliveInterval = TimeSpan.FromSeconds(120),
        };
        app.UseWebSockets(webSocketOptions);

        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseRouting();
        app.UseMiddleware<AuthMiddleware>();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


    }
}
   
