using Misa.demo.core.DTOs;
using Misa.demo.core.Exceptions;
using System.Net;
using System.Text.Json;

namespace Misa_FS.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Đã xảy ra lỗi: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            // Dùng ServiceResponse (Bước 2) để trả về lỗi
            var errorResponse = ServiceResponse<object>.Error(exception.Message);

            switch (exception)
            {
                case ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    errorResponse.Message = ex.Message;
                    break;
                case NotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                    errorResponse.Message = "Lỗi hệ thống, vui lòng liên hệ Misa!";
                    // (Trong môi trường Dev, bạn có thể trả về ex.Message)
                    // errorResponse.Message = ex.Message; 
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Giống project tham khảo
            });
            await context.Response.WriteAsync(result);
        }
    }
}