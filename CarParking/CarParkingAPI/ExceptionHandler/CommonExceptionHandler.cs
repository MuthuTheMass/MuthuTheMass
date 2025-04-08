using System.Net;
using System.Text.Json;

namespace CarParkingBooking.ExceptionHandler
{
    public class CommonExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CommonExceptionHandler(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorMessage = ex?.Message ?? "An unknown error occurred";
                var stackTrace = ex?.StackTrace ?? "No stack trace";

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = errorMessage,
                    StackTrace = stackTrace
                }));
            }
        }
    }
}