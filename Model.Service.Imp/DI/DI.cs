using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Services.DI
{

        public static class DIExtension
        {
            public static IServiceCollection AddConfig(
                 this IServiceCollection services, IConfiguration config)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    //options.InstanceName = "redis";
                    options.Configuration = "localhost:6379, password=SUASENHA";
                });

                services.AddDbContext<MyDbContext>(options => options.UseNpgsql(config.GetConnectionString("postgree")));

                return services;
            }
        }
    
}
