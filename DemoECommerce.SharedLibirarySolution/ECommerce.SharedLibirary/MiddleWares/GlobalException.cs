

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

using ECommerce.SharedLibirary.Logs;

namespace ECommerce.SharedLibirary.MiddleWares
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            //try
            //{
            //    await next(context);
            //}
            //catch (Exception ex)
            //{
            //    context.Response.StatusCode = 500;
            //    context.Response.ContentType = "application/json";
            //    var response = new { message = "An unexpected error occurred.", detail = ex.Message };
            //    await context.Response.WriteAsJsonAsync(response);
            //}


            string message = "Sorry , internal server Error";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string Title = "Error";
            try
            {
                await next(context);

                //check if exception is too many requests 429
                if (context.Response.StatusCode == (int)HttpStatusCode.TooManyRequests)
                {
                    message = "Too many requests. Please try again later.";
                    statusCode = (int)HttpStatusCode.TooManyRequests;
                    Title = "Too Many Requests";
                    await ModifyHeader(context, Title, message, statusCode);
                }
                //unAuthorized 401
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    message = "You are not authorized to access this resource.";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    Title = "Unauthorized";
                    await ModifyHeader(context, Title, message, statusCode);
                }
                //forbidden 403
                if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    message = "You do not have permission to access this resource.";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    Title = "Forbidden";
                    await ModifyHeader(context, Title, message, statusCode);
                }
                //not found
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    message = "The requested resource was not found.";
                    statusCode = (int)HttpStatusCode.NotFound;
                    Title = "Not Found";
                    await ModifyHeader(context, Title, message, statusCode);
                }
                //bad request
                // 400
                if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest)
                {
                    message = "The request was invalid or cannot be served.";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    Title = "Bad Request";
                    await ModifyHeader(context, Title, message, statusCode);
                }
                // other status codes can be handled similarly
                


            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                LogExceptions.LogException(ex); //using serilog
                // You can customize statusCode and Title based on exception type if needed


                //check if exception is timeout 

                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    message = "The request timed out. Please try again later.";
                    Title = "Request Time Out";
                    statusCode = StatusCodes.Status408RequestTimeout;
                    await ModifyHeader(context, Title, message, statusCode);

                }
            }
        }


        private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Title = title,
                Status = statusCode,
                Detail = message
            }), CancellationToken.None);


            return;

        }

    }



}

