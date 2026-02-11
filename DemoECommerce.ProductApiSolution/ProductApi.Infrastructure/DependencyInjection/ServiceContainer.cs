using ECommerce.SharedLibirary.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            // For example:
            // services.AddScoped<IProductRepository, ProductRepository>();
            //add the data base connectivity and the auth scheme ==> from the shared service container
             SharedServiceContainer.AddSharedServices<ProductDbContext>(services, configuration, configuration["MySerilog:FileName"]);




             



        }
    }
}
}
