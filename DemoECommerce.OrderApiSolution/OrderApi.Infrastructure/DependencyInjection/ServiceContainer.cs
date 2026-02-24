using ECommerce.SharedLibirary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Application.Interfaces;
using OrderApi.Infrastructure.Data;
using OrderApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {

        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {





            //add the shared service 
            SharedServiceContainer.AddSharedServices<OrderDbContext>(services, configuration, configuration["MySerilog:FileName"]!);
            // Register the OrderRepository as the implementation of IOrder
            services.AddScoped<IOrder, OrderRepository>();
            // You can also register other infrastructure services here if needed


        }


        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            //register middle wares ==> in shared 
            //handle external errors and log them in a file using serilog
            // listen to only api gateway and order api
            SharedServiceContainer.UseSharedPolicies(app);

            return app;
        }
    }
}


