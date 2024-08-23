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
            catch (BadHttpRequestException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message,
                });
            }
        }
    }
}
