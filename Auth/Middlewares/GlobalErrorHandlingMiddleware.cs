using Microsoft.AspNetCore.Mvc;

namespace Auth.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new ProblemDetails
                {
                    Status = 500,
                    Title = "Error",
                    Detail = ex.Message
                };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
