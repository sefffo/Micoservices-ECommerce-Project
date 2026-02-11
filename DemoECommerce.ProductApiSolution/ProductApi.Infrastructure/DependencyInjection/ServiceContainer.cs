using ECommerce.SharedLibirary.DependencyInjection;
using ECommerce.SharedLibirary.MiddleWares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services here
            // For example:
            // services.AddScoped<IProductRepository, ProductRepository>();
            //add the data base connectivity and the auth scheme ==> from the shared service container
            SharedServiceContainer.AddSharedServices<ProductDbContext>(services, configuration, configuration["MySerilog:FileName"]);
            //create the dependency injection for the product repository
            services.AddScoped<IProduct, ProductRepository>();

            return services;

        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Configure middleware or other infrastructure-related components here
            //app.UseMiddleware<GlobalException>();

            // For example:
            // app.UseMiddleware<YourCustomMiddleware>();

            //use only wht makes the request come from the api gateway

            //app.UseMiddleware<ListenToOnlyApiGateway>();

            SharedServiceContainer.UseSharedPolicies(app);

            return app;

        }
    }
}

