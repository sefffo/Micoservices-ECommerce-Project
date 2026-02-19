using ECommerce.SharedLibirary.Logs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OrderApi.Application.services;
using Polly;
using Polly.Retry;

namespace OrderApi.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services , IConfiguration configuration)
        {
            //register the services used 
            services.AddHttpClient<IOrderService, OrderService>(options => {
                options.BaseAddress = new Uri(configuration["ApiGateway:BaseAddress"]!);
                options.Timeout = TimeSpan.FromSeconds(2);




                });



            //create the DI
            var retryStrat = new RetryStrategyOptions()
            {

                ShouldHandle = new PredicateBuilder().Handle<TaskCanceledException>(),
                BackoffType = DelayBackoffType.Constant
                ,UseJitter = true
                ,MaxRetryAttempts = 3,
                Delay = TimeSpan.FromMilliseconds(500),
                OnRetry = args =>
                {
                    string message = $"OnRetry , Attempt : {args.AttemptNumber} OutCome {args.Outcome}";
                    LogExceptions.LogToConsole(message);
                    LogExceptions.LogToDebug(message);
                    return ValueTask.CompletedTask;
                }
            };


            //use this 

            services.AddResiliencePipeline("my-retry-pipeline",builder=>builder.AddRetry(retryStrat));

            return services;
        }
    }
}
