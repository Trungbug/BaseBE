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

        /// <summary>
        /// Hàm tạo ExceptionHandlingMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Hàm xử lý ngoại lệ
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Hàm xử lý ngoại lệ và trả về phản hồi phù hợp
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;
            var errorResponse = ServiceResponse<object>.Error(exception.Message);

            switch (exception)
            {
                case ValidationException ex:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = ex.Message;
                    break;
                case NotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound; 
                    errorResponse.Message = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    errorResponse.Message = "Lỗi hệ thống, vui lòng liên hệ Misa!";
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            });
            await context.Response.WriteAsync(result);
        }
    }
}