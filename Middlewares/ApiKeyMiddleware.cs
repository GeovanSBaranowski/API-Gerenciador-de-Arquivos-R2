using Microsoft.AspNetCore.Authorization;
using UploadImagemR2.Constants;
using UploadImagemR2.DTOs.Responses;
using UploadImagemR2.Exceptions;
using UploadImagemR2.Services.Interfaces;

namespace UploadImagemR2.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAplicacaoService service)
        {
            var endpoint = httpContext.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

            if (!allowAnonymous)
            {
                if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKeyValue))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse
                    {
                        Status = httpContext.Response.StatusCode,
                        Message = "Autenticacao Obrigatoria"
                    });

                    return;
                }

                if (string.IsNullOrWhiteSpace(apiKeyValue))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse
                    {
                        Status = httpContext.Response.StatusCode,
                        Message = "Autenticacao Obrigatoria"
                    });

                    return;
                }

                var authConfirmation = await service.AuthenticateApiKeyAsync(apiKeyValue);

                httpContext.Items[HttpContextKeys.Aplicacao] = authConfirmation;
            }
            await _next(httpContext);
        }
    }
}