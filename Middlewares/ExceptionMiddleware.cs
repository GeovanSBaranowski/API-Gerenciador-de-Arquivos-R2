using UploadImagemR2.DTOs.Responses;
using UploadImagemR2.Exceptions;

namespace UploadImagemR2.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
                if(ex is AppException appException)
                {
                    httpContext.Response.StatusCode = (int)appException.StatusCode;
                    await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse{ 
                        Status = httpContext.Response.StatusCode,
                        Error = appException.GetType().Name,
                        Message = appException.Message 
                        });

                return;

                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await httpContext.Response.WriteAsJsonAsync(new { message = "Ocorreu um erro interno no servidor." });
                }
            }
        }
    }
}